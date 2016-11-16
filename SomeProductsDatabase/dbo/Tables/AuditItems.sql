﻿CREATE TABLE [dbo].[AuditItems]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [AuditEntityId] INT NOT NULL, 
    [EntityId] INT NOT NULL, 
    [ModifiedField] NVARCHAR(50) NULL, 
    [PreviousValue] NVARCHAR(MAX) NULL, 
    [NextValue] NVARCHAR(MAX) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL DEFAULT GETDATE(), 
    [StatusId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [CompanyId] INT NOT NULL DEFAULT 2 , 
    CONSTRAINT [FK_AuditItems_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_AuditItems_AuditEntity] FOREIGN KEY ([AuditEntityId]) REFERENCES [AuditEntities]([Id]), 
    CONSTRAINT [FK_AuditItems_AuditStatus] FOREIGN KEY ([StatusId]) REFERENCES [AuditStatus]([Id]), 
    CONSTRAINT [FK_AuditItems_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [Companies]([Id]),
    CONSTRAINT [PK_AuditItems] PRIMARY KEY ([Id])
)
