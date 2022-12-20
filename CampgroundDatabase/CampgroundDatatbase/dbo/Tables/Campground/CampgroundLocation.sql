CREATE TABLE [dbo].[CampgroundLocation]
(
	[CampgroundId] INT NOT NULL, 
    [Lat] FLOAT NOT NULL, 
    [Long] FLOAT NOT NULL,
    PRIMARY KEY ([CampgroundId], [Lat], [Long]),
    FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
