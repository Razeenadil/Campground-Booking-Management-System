CREATE TABLE [dbo].[AmenityLake]
(
	[Name] VARCHAR(50) NOT NULL, 
    [Depth] FLOAT NOT NULL, 
    [Area] FLOAT NOT NULL, 
    [BoatingAllowed] BIT NOT NULL, 
    [FishingAllowed] BIT NOT NULL,
    PRIMARY KEY ([Name]),
    FOREIGN KEY ([Name]) REFERENCES Amenity([Name]) ON DELETE CASCADE
);
