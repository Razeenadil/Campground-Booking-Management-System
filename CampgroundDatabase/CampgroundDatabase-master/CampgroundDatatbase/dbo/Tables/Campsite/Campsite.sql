CREATE TABLE [dbo].[Campsite]
(
	[SiteNo] INT NOT NULL IDENTITY(1000,3), 
    [CampgroundId] INT NOT NULL
	PRIMARY KEY ([SiteNo]),
	FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
