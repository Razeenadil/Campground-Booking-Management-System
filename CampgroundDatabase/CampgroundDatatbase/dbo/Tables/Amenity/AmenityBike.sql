CREATE TABLE [dbo].[AmenityBike]
(
	[Name] VARCHAR(50) NOT NULL, 
    [Experience] VARCHAR(25) NOT NULL, 
    [Distance] FLOAT NOT NULL,
    PRIMARY KEY ([Name]),
    FOREIGN KEY ([Name]) REFERENCES Amenity([Name]) ON DELETE CASCADE
);
