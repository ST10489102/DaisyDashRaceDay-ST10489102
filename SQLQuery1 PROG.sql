CREATE DATABASE DaisyDashRaceDay;
GO

USE DaisyDashRaceDay;
GO

-- 1. USERS
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Organiser', 'Participant')),
    Phone NVARCHAR(20) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- 2. EVENTS
CREATE TABLE Events (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    EventDate DATETIME NOT NULL,
    RegistrationDeadline DATETIME NOT NULL,
    MaxParticipants INT NOT NULL CHECK (MaxParticipants > 0),
    OrganiserID INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Events_Organiser FOREIGN KEY (OrganiserID) REFERENCES Users(UserID),
    CONSTRAINT CHK_DeadlineBeforeEvent CHECK (RegistrationDeadline < EventDate)
);
GO

-- 3. CATEGORIES
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    CategoryName NVARCHAR(100) NOT NULL,
    Distance DECIMAL(5,2) NOT NULL CHECK (Distance > 0),
    EntryFee DECIMAL(10,2) NOT NULL DEFAULT 0,
    MaxSlots INT NOT NULL CHECK (MaxSlots > 0),
    StartTime TIME NULL,
    CONSTRAINT FK_Categories_Event FOREIGN KEY (EventID) REFERENCES Events(EventID) ON DELETE CASCADE,
    CONSTRAINT UQ_Event_Category UNIQUE (EventID, CategoryName)
);
GO

-- 4. ENROLMENTS
CREATE TABLE Enrolments (
    EnrolmentID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    EventID INT NOT NULL,
    CategoryID INT NOT NULL,
    BibNumber NVARCHAR(20) NOT NULL UNIQUE,
    EmergencyContact NVARCHAR(100) NOT NULL,
    MedicalInfo NVARCHAR(255) NULL,
    EnrolmentDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Confirmed' CHECK (Status IN ('Confirmed', 'Cancelled', 'Completed')),
    CONSTRAINT FK_Enrolments_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Enrolments_Event FOREIGN KEY (EventID) REFERENCES Events(EventID),
    CONSTRAINT FK_Enrolments_Category FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    CONSTRAINT UQ_User_Category UNIQUE (UserID, CategoryID)
);
GO

-- 5. RESULTS
CREATE TABLE Results (
    ResultID INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentID INT NOT NULL UNIQUE,
    FinishTime TIME NOT NULL,
    Position INT NULL CHECK (Position > 0),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Finished' CHECK (Status IN ('Finished', 'DNF', 'DQ')),
    CONSTRAINT FK_Results_Enrolment FOREIGN KEY (EnrolmentID) REFERENCES Enrolments(EnrolmentID)
);
GO

-- 6. PAYMENTS
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL CHECK (Amount >= 0),
    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'Card',
    PaymentStatus NVARCHAR(20) NOT NULL DEFAULT 'Paid' CHECK (PaymentStatus IN ('Paid', 'Pending', 'Failed')),
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Payments_Enrolment FOREIGN KEY (EnrolmentID) REFERENCES Enrolments(EnrolmentID)
);
GO

-- SEED DATA
INSERT INTO Users (FullName, Email, PasswordHash, Role, Phone) VALUES
('Thandi Nkosi', 'thandi.organiser@daisydash.co.za', 'hashed_password_1', 'Organiser', '0821234567'),
('James Muller', 'james.organiser@daisydash.co.za', 'hashed_password_2', 'Organiser', '0832345678'),
('Sipho Dlamini', 'sipho.runner@gmail.com', 'hashed_password_3', 'Participant', '0843456789'),
('Aisha Patel', 'aisha.runner@gmail.com', 'hashed_password_4', 'Participant', '0714567890');

INSERT INTO Events (Title, Description, Location, EventDate, RegistrationDeadline, MaxParticipants, OrganiserID) VALUES
('DaisyDash Spring 5K Fun Run', 'Annual spring family fun run', 'Johannesburg Botanical Gardens', '2026-09-20 07:00:00', '2026-09-15 23:59:00', 500, 1),
('DaisyDash Summer Challenge', 'Competitive 10K and half-marathon', 'Cape Town Promenade', '2026-11-10 06:00:00', '2026-11-05 23:59:00', 1000, 1),
('DaisyDash Trail Adventure', 'Off-road trail running', 'Drakensberg Trails, KZN', '2026-12-05 05:30:00', '2026-11-30 23:59:00', 300, 2);

INSERT INTO Categories (EventID, CategoryName, Distance, EntryFee, MaxSlots, StartTime) VALUES
(1, '5K Fun Run', 5.00, 150.00, 300, '07:30:00'),
(1, '2K Kids Dash', 2.00, 80.00, 200, '07:00:00'),
(2, '10K Challenge', 10.00, 250.00, 500, '06:30:00'),
(2, '21K Half Marathon', 21.10, 400.00, 300, '06:00:00'),
(2, '5K Social Run', 5.00, 150.00, 200, '07:00:00'),
(3, '15K Trail', 15.00, 350.00, 150, '06:00:00'),
(3, '30K Ultra Trail', 30.00, 600.00, 100, '05:30:00'),
(3, '8K Forest Loop', 8.00, 200.00, 50, '06:30:00');

INSERT INTO Enrolments (UserID, EventID, CategoryID, BibNumber, EmergencyContact, MedicalInfo) VALUES
(3, 1, 1, 'DD2026-0001', 'Nomsa Dlamini - 0821112222', 'No allergies'),
(3, 2, 3, 'DD2026-0002', 'Nomsa Dlamini - 0821112222', 'No allergies'),
(4, 1, 1, 'DD2026-0003', 'Yusuf Patel - 0712223333', 'Asthma inhaler'),
(4, 2, 5, 'DD2026-0004', 'Yusuf Patel - 0712223333', 'Asthma inhaler'),
(4, 3, 8, 'DD2026-0005', 'Yusuf Patel - 0712223333', 'Asthma inhaler');

INSERT INTO Results (EnrolmentID, FinishTime, Position, Status) VALUES
(1, '00:28:45', 12, 'Finished'),
(3, '00:32:10', 25, 'Finished');

INSERT INTO Payments (EnrolmentID, Amount, PaymentMethod, PaymentStatus) VALUES
(1, 150.00, 'Card', 'Paid'),
(2, 250.00, 'EFT', 'Paid'),
(3, 150.00, 'Card', 'Paid'),
(4, 150.00, 'Card', 'Paid'),
(5, 200.00, 'Card', 'Paid');

SELECT * FROM Users;
SELECT * FROM Events;
SELECT * FROM Categories;
SELECT * FROM Enrolments;