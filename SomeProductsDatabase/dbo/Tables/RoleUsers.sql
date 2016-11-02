CREATE TABLE [dbo].[UserRoles]
(
	[Id] INT IDENTITY,
	[User_Id] INT NOT NULL, 
    [Role_Id] INT NOT NULL,
	CONSTRAINT [PK_RoleUser] PRIMARY KEY CLUSTERED ([Id]), 
    CONSTRAINT [FK_RoleUsers_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users]([Id]), 
    CONSTRAINT [FK_RoleUsers_Roles_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles]([Id])
)
