CREATE TABLE [dbo].[PermissionRole]
(
    [PermissionId] INT NULL, 
    [RoleId] INT NULL, 
    CONSTRAINT [FK_PermissionRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id]), 
    CONSTRAINT [FK_PermissionRole_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [Permission]([Id])
)
