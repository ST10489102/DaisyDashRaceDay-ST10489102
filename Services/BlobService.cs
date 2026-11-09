using Microsoft.AspNetCore.Http;

namespace DaisyDashRaceDay.API.Services;

public class BlobService
{
    private readonly string _uploadsFolder;

    public BlobService(IConfiguration config)
    {
        // Save to wwwroot/uploads so it can be served as /uploads/filename
        _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(_uploadsFolder);
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return a local URL instead of an Azure Blob URL
        return "/uploads/" + fileName;
    }
}