SELECT dbo.Factory.Id, dbo.Factory.Name, b.SumVolume as Volume, b.SumMaxVolume as MaxVolume
FROM dbo.Factory
	inner join (select FactoryId, Sum(SumVolume) as SumVolume, Sum(SumMaxVolume) as SumMaxVolume from (
		SELECT dbo.Unit.Id, dbo.Unit.Name, Unit.FactoryId, b.SumVolume, b.SumMaxVolume
		FROM dbo.Unit
		inner join (select UnitId, Sum(Volume) as SumVolume, Sum(MaxVolume) as SumMaxVolume from dbo.Tank group by UnitId) b on dbo.Unit.id = b.UnitId
	) as a
	group by a.FactoryId) b on dbo.Factory.id = b.FactoryId;