CREATE TABLE [dbo].[Order]
(
	[Id] INt NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL,
    [Date] DATETIME NULL, 
    CONSTRAINT [FK_Order_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
