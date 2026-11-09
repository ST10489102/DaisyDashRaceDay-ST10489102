using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyDashRaceDay.API.Migrations
{
    /// <inheritdoc />
    public partial class Part3_ModelFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Categories_CategoryId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Events_EventId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Results",
                newName: "ResultId");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrolmentId1",
                table: "Results",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrganiserId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Enrolments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "Enrolments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results",
                column: "EnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_EnrolmentId1",
                table: "Results",
                column: "EnrolmentId1",
                unique: true,
                filter: "[EnrolmentId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Results_EventId",
                table: "Results",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_CategoryId1",
                table: "Enrolments",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_EventId1",
                table: "Enrolments",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId1",
                table: "Categories",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Events_EventId1",
                table: "Categories",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Categories_CategoryId",
                table: "Enrolments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Categories_CategoryId1",
                table: "Enrolments",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Events_EventId",
                table: "Enrolments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Events_EventId1",
                table: "Enrolments",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Enrolments_EnrolmentId",
                table: "Results",
                column: "EnrolmentId",
                principalTable: "Enrolments",
                principalColumn: "EnrolmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Enrolments_EnrolmentId1",
                table: "Results",
                column: "EnrolmentId1",
                principalTable: "Enrolments",
                principalColumn: "EnrolmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Events_EventId",
                table: "Results",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Users_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Events_EventId1",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Categories_CategoryId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Categories_CategoryId1",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Events_EventId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Events_EventId1",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Enrolments_EnrolmentId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Enrolments_EnrolmentId1",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Events_EventId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Users_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EnrolmentId1",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EventId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Enrolments_CategoryId1",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolments_EventId1",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EventId1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrolmentId1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OrganiserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Enrolments");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Enrolments");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ResultId",
                table: "Results",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Categories_CategoryId",
                table: "Enrolments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Events_EventId",
                table: "Enrolments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
