CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(100) NULL, 
    [Price] INT NULL, 
    [ImageSource] VARCHAR(50) NULL,
)
