CREATE TABLE [dbo].[CampgroundOpenDuring]
(
	[CampgroundId] INT NOT NULL, 
    [OpenDate] DATE NOT NULL, 
    [CloseDate] DATE NOT NULL,
    PRIMARY KEY ([CampgroundId], [OpenDate], [CloseDate]),
    FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
