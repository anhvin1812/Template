CREATE TABLE [dbo].[Setting]
(
	[Id] INT NOT NULL IDENTITY, 
    [Menu] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(255) NULL, 
    [PhoneNumber] NVARCHAR(255) NULL, 
    [Skype] NVARCHAR(255) NULL, 
    [Email] NVARCHAR(255) NULL, 
    [Facebook] NVARCHAR(2000) NULL, 
    [Website] NVARCHAR(255) NULL, 
    [Logo] NVARCHAR(255) NULL, 

    CONSTRAINT [PK_Setting] PRIMARY KEY ([Id])
)
