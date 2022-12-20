CREATE TABLE [dbo].[CampgroundServices]
(
	[SiteService] VARCHAR(50) NOT NULL, 
    [CampgroundId] INT NOT NULL,
	PRIMARY KEY ([SiteService],[CampgroundId]),
	FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
