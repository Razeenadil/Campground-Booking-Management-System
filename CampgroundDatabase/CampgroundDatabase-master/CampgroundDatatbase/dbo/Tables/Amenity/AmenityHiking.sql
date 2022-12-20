CREATE TABLE [dbo].[AmenityHiking]
(
	[Name] VARCHAR(50) NOT NULL, 
    [Experience] VARCHAR(25) NOT NULL, 
    [Elevation] FLOAT NOT NULL,
    PRIMARY KEY ([Name]),
    FOREIGN KEY ([Name]) REFERENCES Amenity([Name]) ON DELETE CASCADE
);
