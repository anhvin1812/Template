CREATE TABLE [dbo].[Permission]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ClaimType] NVARCHAR(100) NULL, 
    [ClaimValue] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(MAX) NULL
)
