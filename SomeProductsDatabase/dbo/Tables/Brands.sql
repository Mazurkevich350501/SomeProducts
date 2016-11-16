CREATE TABLE [dbo].[Brands] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    [CreateDate] DATETIME NOT NULL , 
    [ModifiedDate] DATETIME NULL , 
    [RowVersion] ROWVERSION NOT NULL , 
    [CompanyId] INT NOT NULL , 
    [ActiveStateId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Brands_BrandId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Brands_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id]), 
    CONSTRAINT [AK_Brands_Name_CompanyId] UNIQUE ([Name], [CompanyId]),
    CONSTRAINT [FK_Brands_ActiveStates] FOREIGN KEY ([ActiveStateId]) REFERENCES [ActiveStates]([Id]),
);


GO
