SELECT dbo.Unit.Id, dbo.Unit.Name, dbo.Factory.Name as Name_of_factory
FROM dbo.Unit
INNER JOIN dbo.Factory ON dbo.Unit.FactoryId = dbo.Factory.Id;