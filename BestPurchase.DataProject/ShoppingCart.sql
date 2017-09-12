CREATE TABLE [dbo].[ShoppingCart]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT NOT NULL, 
    [Quantity] INT NULL, 
    CONSTRAINT [FK_ShoppingCart_ToProduct] FOREIGN KEY (ProductId) REFERENCES [Product]([Id])
)
