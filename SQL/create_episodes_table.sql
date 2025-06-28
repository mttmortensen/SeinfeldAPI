CREATE TABLE Episodes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Season NVARCHAR(10),
    EpisodeNumber NVARCHAR(10),
    AirDate DATE
);
