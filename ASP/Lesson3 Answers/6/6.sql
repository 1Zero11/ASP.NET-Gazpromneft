DELETE FROM Factory
    WHERE ID NOT IN
    (
        SELECT MIN(ID) AS MinRecordID
        FROM Factory
        GROUP BY [Name], 
                 [Description]
    );
