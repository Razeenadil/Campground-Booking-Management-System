CREATE TABLE [dbo].[AmenityStore]
(
	[Name] VARCHAR(50) NOT NULL, 
    [Hours] TIME NOT NULL,
	PRIMARY KEY ([Name]),
    FOREIGN KEY ([Name]) REFERENCES Amenity([Name]) ON DELETE CASCADE
);
