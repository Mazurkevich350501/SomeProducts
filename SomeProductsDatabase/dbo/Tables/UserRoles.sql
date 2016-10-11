CREATE TABLE [dbo].[UserRoles]
(
	[Id] INT IDENTITY,
	[User_Id] INT NOT NULL, 
    [Role_Id] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([Id]), 
    CONSTRAINT [FK_UserRoles_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users]([Id]), 
    CONSTRAINT [FK_UserRoles_Roles_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles]([Id])
)
