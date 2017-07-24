CREATE TABLE [dbo].[Tag]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [IsDisabled] BIT NULL, 

    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id])
)
