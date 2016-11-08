CREATE TABLE [dbo].[AuditStatuses]
(
	[Id] INT NOT NULL , 
    [Value] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_AuditStatuses] PRIMARY KEY ([Id])
)
