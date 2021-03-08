SELECT *
FROM dbo.Unit
	inner join (
		SELECT UnitId
		FROM dbo.Tank
		WHERE Volume > 1000
		GROUP BY UnitId
	) b on Unit.Id = b.UnitId;