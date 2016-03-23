CREATE TABLE [dbo].[UserClaim]
(
    [Id] INT NOT NULL,
	[UserId] INT NOT NULL,
    [ClaimType] NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,

    CONSTRAINT [PK_UserClaim_ClaimId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);
