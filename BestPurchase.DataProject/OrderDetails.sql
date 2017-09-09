CREATE TABLE [dbo].[OrderDetails]
(
	[OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NULL, 
    CONSTRAINT [FK_OrderDetails_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]), 
    CONSTRAINT [FK_OrderDetails_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]),
	PRIMARY KEY(OrderId, ProductId)
)
