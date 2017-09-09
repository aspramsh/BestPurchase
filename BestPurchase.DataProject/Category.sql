CREATE TABLE [dbo].[Category]
(
	[ProductId] INT NOT NULL PRIMARY KEY,
    [ProductCategory] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Category_ToProduct] FOREIGN KEY (ProductId) REFERENCES Product(Id),
)
