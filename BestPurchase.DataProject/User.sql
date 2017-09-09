CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [Addres] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NULL, 
    [Country] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Phone] VARCHAR(50) NULL, 
    [PostalCode] VARCHAR(10) NULL, 
    [State] VARCHAR(50) NULL, 
    [Username] VARCHAR(50) NULL
)
