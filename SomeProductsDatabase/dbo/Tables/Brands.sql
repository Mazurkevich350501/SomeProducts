CREATE TABLE [dbo].[Brands] (
    [BrandId]   INT            IDENTITY (1, 1) NOT NULL,
    [BrandName] NVARCHAR (200) NOT NULL,
    [CreateDate] DATETIME NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    CONSTRAINT [PK_table1_BrandId] PRIMARY KEY CLUSTERED ([BrandId] ASC)
);

