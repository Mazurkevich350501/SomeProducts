CREATE TABLE [dbo].[Products] (
    [Id]   INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NOT NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Color]       NVARCHAR (50)   NOT NULL,
    [Quantity]    INT             NOT NULL,
    [Image]       VARBINARY (MAX) NULL,
    [ImageType]   NVARCHAR (MAX)  NULL,
    [BrandId]     INT             NOT NULL,
	[CreateDate] DATETIME NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [RowVersion] ROWVERSION NOT NULL , 
    [CompanyId] INT NOT NULL, 
    CONSTRAINT [PK_Products_ProductId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([Id]),
	CONSTRAINT [FK_Products_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies]([Id]),
);

