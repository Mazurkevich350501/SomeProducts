﻿CREATE TABLE [dbo].[Companies]
(
	[Id] INT NOT NULL IDENTITY, 
    [CompanyName] NVARCHAR(MAX) NOT NULL, 
	CONSTRAINT [PK_Companies_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
)