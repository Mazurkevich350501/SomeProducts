CREATE TABLE [dbo].[Brands] (
    [BrandId]   INT            IDENTITY (1, 1) NOT NULL,
    [BrandName] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_table1_BrandId] PRIMARY KEY CLUSTERED ([BrandId] ASC)
);

