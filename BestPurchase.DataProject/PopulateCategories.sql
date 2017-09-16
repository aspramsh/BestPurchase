CREATE PROCEDURE [dbo].[PopulateCategories]
AS
BEGIN
	INSERT INTO [dbo].[Category]
           ([ProductId]
           ,[ProductCategory])
     VALUES
           (1, 'MeatProducts'),
		   (2, 'FrozenProducts'),
		   (3, 'Sweets'),
		   (4, 'FishAndSeaFood'),
		   (5, 'OrganicProducts')
END
