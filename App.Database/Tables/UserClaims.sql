﻿CREATE TABLE [dbo].[UserClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
    [ClaimType] NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,

    CONSTRAINT [PK_UserClaim_ClaimID] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);
