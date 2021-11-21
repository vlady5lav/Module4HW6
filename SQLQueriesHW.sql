-----------
-- Query 1
-- Actions with determining whether the author is alive or not
-- are performed on the client side (after .ToList ()) to bypass
-- the output of songs with several artists, among which there are
-- both alive and not
-----------

SELECT [s].[Title], [g].[Title] AS [Genre], [s].[ReleasedDate], [s].[Duration], [a0].[ArtistId], [a0].[DateOfBirth], [a0].[DateOfDeath], [a0].[Email], [a0].[InstagramUrl], [a0].[IsAlived], [a0].[Name], [a0].[Phone]
FROM [Song] AS [s]
INNER JOIN [ArtistSong] AS [a] ON [s].[SongId] = [a].[SongId]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
WHERE [s].[GenreId] IS NOT NULL
GO

-----------
-- Query 1 (IsAlived (ComputedColumnSql) variant 1)
-----------

SELECT [s].[Title], [g].[Title] AS [Genre], [s].[ReleasedDate], [s].[Duration], [a0].[ArtistId], [a0].[DateOfBirth], [a0].[DateOfDeath], [a0].[Email], [a0].[InstagramUrl], [a0].[IsAlived], [a0].[Name], [a0].[Phone]
FROM [Song] AS [s]
INNER JOIN [ArtistSong] AS [a] ON [s].[SongId] = [a].[SongId]
INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
WHERE [s].[GenreId] IS NOT NULL AND ([a0].[IsAlived] = CAST(1 AS bit))
GO

-----------
-- Query 1 (DateOfDeath variant 1)
-----------

SELECT [s].[Title], [g].[Title] AS [Genre], [s].[ReleasedDate], [s].[Duration], [a0].[ArtistId], [a0].[DateOfBirth], [a0].[DateOfDeath], [a0].[Email], [a0].[InstagramUrl], [a0].[IsAlived], [a0].[Name], [a0].[Phone]
FROM [Song] AS [s]
INNER JOIN [ArtistSong] AS [a] ON [s].[SongId] = [a].[SongId]
INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
WHERE [s].[GenreId] IS NOT NULL AND [a0].[DateOfDeath] IS NULL
GO

-----------
-- Query 1 (IsAlived (ComputedColumnSql) variant 2)
-----------

SELECT [s].[Title], [g].[Title] AS [Genre], [s].[ReleasedDate], [s].[Duration], [t].[ArtistId0], [t].[DateOfBirth], [t].[DateOfDeath], [t].[Email], [t].[InstagramUrl], [t].[IsAlived], [t].[Name], [t].[Phone]
FROM [Song] AS [s]
INNER JOIN (
    SELECT [a].[SongId], [a0].[ArtistId] AS [ArtistId0], [a0].[DateOfBirth], [a0].[DateOfDeath], [a0].[Email], [a0].[InstagramUrl], [a0].[IsAlived], [a0].[Name], [a0].[Phone]
    FROM [ArtistSong] AS [a]
    INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
    WHERE [a0].[IsAlived] = CAST(1 AS bit)
) AS [t] ON [s].[SongId] = [t].[SongId]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
WHERE [s].[GenreId] IS NOT NULL
GO

-----------
-- Query 1 (DateOfDeath variant 2)
-----------

SELECT [s].[Title], [g].[Title] AS [Genre], [s].[ReleasedDate], [s].[Duration], [t].[ArtistId0], [t].[DateOfBirth], [t].[DateOfDeath], [t].[Email], [t].[InstagramUrl], [t].[IsAlived], [t].[Name], [t].[Phone]
FROM [Song] AS [s]
INNER JOIN (
    SELECT [a].[SongId], [a0].[ArtistId] AS [ArtistId0], [a0].[DateOfBirth], [a0].[DateOfDeath], [a0].[Email], [a0].[InstagramUrl], [a0].[IsAlived], [a0].[Name], [a0].[Phone]
    FROM [ArtistSong] AS [a]
    INNER JOIN [Artist] AS [a0] ON [a].[ArtistId] = [a0].[ArtistId]
    WHERE [a0].[DateOfDeath] IS NULL
) AS [t] ON [s].[SongId] = [t].[SongId]
LEFT JOIN [Genre] AS [g] ON [s].[GenreId] = [g].[GenreId]
WHERE [s].[GenreId] IS NOT NULL
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
    SELECT MIN([a].[DateOfBirth])
    FROM [Artist] AS [a])
GO
