CREATE TABLE [dbo].[Companies]
(
	[Id] INT NOT NULL IDENTITY, 
    [CompanyName] NVARCHAR(MAX) NOT NULL, 
	[ActiveStateId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Companies_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Companies_ActiveStates] FOREIGN KEY ([ActiveStateId]) REFERENCES [ActiveStates]([Id]),
)
