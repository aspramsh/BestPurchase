CREATE PROCEDURE [dbo].[PopulateProductTable]
AS
BEGiN
	INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Price]
           ,[ImageSource])
     VALUES
           ('3 Types Of Meat', 'Beef, Pork And Chicken', 3500, 'meats.jpg'),
		   ('Pizza For Dreamers', '12 Pieces', 2000, 'Frozen.jpg'),
		   ('Charlie''s Chocolate', '5 types of chocolate with cinnamon flavour', 8000, 'chocolate.jpg'),
		   ('Gold Fish', '5 types of fresh and tasty fish', 5000, 'seafood.jpg'),
		   ('Scent of Nature', 'Herbal shampoo for your beauty and health', 4000, 'Shampoo.jpg')
END
