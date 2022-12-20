CREATE TABLE [dbo].[Amenity]
(
	[Name] VARCHAR(50) NOT NULL, 
    [Description] TEXT NULL, 
    [CampgroundId] INT NOT NULL,
    PRIMARY KEY ([Name]),
    FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
