CREATE TABLE [dbo].[CampgroundReview]
(
	[CampgroundId] INT NOT NULL, 
    [PersonEmail] VARCHAR(30) NOT NULL,
    [ReviewId] INT NOT NULL IDENTITY(100,4),
    [Description] TEXT NOT NULL, 
    [StarRating] INT NOT NULL,
    PRIMARY KEY ([CampgroundId], [PersonEmail], [ReviewId]),
	FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE,
	FOREIGN KEY ([PersonEmail]) REFERENCES Person([Email]) ON DELETE CASCADE
);
