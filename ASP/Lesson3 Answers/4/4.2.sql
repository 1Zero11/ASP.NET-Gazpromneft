SELECT dbo.Unit.Id, dbo.Unit.Name, Factory.Name, Factory.Description, b.SumVolume, b.SumMaxVolume
FROM dbo.Unit
INNER JOIN dbo.Factory ON dbo.Unit.FactoryId = dbo.Factory.Id
INNER JOIN (select UnitId, Sum(Volume) as SumVolume, Sum(MaxVolume) as SumMaxVolume from dbo.Tank group by UnitId) as b ON b.UnitId = dbo.Unit.Id;