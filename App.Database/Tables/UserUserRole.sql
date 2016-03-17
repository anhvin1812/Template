CREATE TABLE [dbo].[UserUserRole]
(
    [UserId] NVARCHAR(128) NOT NULL,
    [RoleId] NVARCHAR(128)NOT NULL,

    CONSTRAINT [PK_UserUserRole_UserId_RoleId] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserUserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserUserRole_UserRole] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([Id]) ON DELETE CASCADE
);

