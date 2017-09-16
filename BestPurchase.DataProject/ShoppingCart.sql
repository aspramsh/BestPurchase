CREATE TABLE [dbo].[ShoppingCart]
(
    [Id] VARCHAR(50) NOT NULL,
	[ProductId] INT NOT NULL, 
    [Quantity] INT NULL, 
    CONSTRAINT [FK_ShoppingCart_ToProduct] FOREIGN KEY (ProductId) REFERENCES [Product]([Id]),
	PRIMARY KEY(ProductId, Id)
)
