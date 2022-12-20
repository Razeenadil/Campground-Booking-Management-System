CREATE TABLE [dbo].[CampsiteTenting]
(
	[SiteNo] INT NOT NULL, 
    [TentsPerSite] INT NOT NULL,
	PRIMARY KEY ([SiteNo]),
	FOREIGN KEY ([SiteNo]) REFERENCES Campsite([SiteNo]) ON DELETE CASCADE
);
