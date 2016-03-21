CREATE TABLE [dbo].[UserLogin]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,

    CONSTRAINT [PK_UserLogin_UserId_LoginProvider_ProviderKey] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);
