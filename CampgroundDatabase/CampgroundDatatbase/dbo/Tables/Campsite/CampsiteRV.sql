CREATE TABLE [dbo].[CampsiteRV]
(
	[SiteNo] INT NOT NULL, 
    [PowerHookUp] BIT NOT NULL, 
    [WasteDump] BIT NOT NULL, 
    [WaterHookUp] BIT NOT NULL, 
    [MaxRVSize] INT NOT NULL,
    PRIMARY KEY ([SiteNo]),
	FOREIGN KEY ([SiteNo]) REFERENCES Campsite([SiteNo]) ON DELETE CASCADE
);
