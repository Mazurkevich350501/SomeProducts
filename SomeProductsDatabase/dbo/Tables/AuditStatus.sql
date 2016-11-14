CREATE TABLE [dbo].[AuditStatus]
(
	[Id] INT NOT NULL , 
    [Value] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_AuditStatus] PRIMARY KEY ([Id])
)
