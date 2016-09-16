CREATE TABLE [dbo].[Brands] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    [CreateDate] DATETIME NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [RowVersion] ROWVERSION NOT NULL , 
    CONSTRAINT [PK_table1_BrandId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

