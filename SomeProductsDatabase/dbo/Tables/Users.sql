CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY, 
    [UserName] NVARCHAR(MAX) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL,
	[CompanyId] INT NULL, 
    CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Users_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies]([Id]),
)
