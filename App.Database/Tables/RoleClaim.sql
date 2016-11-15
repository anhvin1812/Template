CREATE TABLE [dbo].[RoleClaim]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [RoleId] INT NOT NULL, 
    [ClaimType] NVARCHAR(255) NOT NULL, 
    [ClaimValue] NVARCHAR(255) NOT NULL, 

    CONSTRAINT [FK_RoleClaim_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
	
)
