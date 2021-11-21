-----------
-- Query 1
-----------

SELECT [s].[SongId], [s].[Duration], [s].[GenreId], [s].[ReleasedDate], [s].[Title]
FROM [Song] AS [s]
WHERE [s].[GenreId] IS NOT NULL AND NOT (EXISTS (
    SELECT 1
    FROM [ArtistSong] AS [a]
    INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
    WHERE ([s].[SongId] = [a].[SongId]) AND ([a0].[IsAlived] <> CAST(1 AS bit))))
GO

-----------
-- Query 2
-----------

SELECT [g].[Title], COUNT(*) AS [Count]
FROM [Song] AS [s]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
GROUP BY [g].[Title]
GO

-----------
-- Query 3
-----------

SELECT [s].[SongId], [s].[Duration], [s].[GenreId], [s].[ReleasedDate], [s].[Title]
FROM [Song] AS [s]
WHERE [s].[ReleasedDate] < (
    SELECT MAX([a].[DateOfBirth])
    FROM [Artist] AS [a])
GO
