CREATE TABLE [dbo].[User]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Firstname] NVARCHAR(255) NULL, 
    [Lastname] NVARCHAR(255) NULL, 
	[Address] NVARCHAR(500) NULL, 
    [UserName] NVARCHAR(256) NOT NULL,
    [Email] NVARCHAR(256) NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR (100) NULL,
    [SecurityStamp] NVARCHAR (100) NULL,
    [PhoneNumber] NVARCHAR(256) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEndDateUtc] DATETIME NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,
    
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_User_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);
