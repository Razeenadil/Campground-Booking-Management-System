CREATE TABLE [dbo].[CampsiteMaintenance]
(
	[SiteNo] INT NOT NULL, 
    [MaintenanceNumber] INT NOT NULL IDENTITY(10000,7), 
    [ESSN] INT NOT NULL, 
    [Date] DATE NOT NULL, 
    [Description] TEXT NOT NULL, 
    PRIMARY KEY ([SiteNo], [MaintenanceNumber], [ESSN]),
	FOREIGN KEY ([SiteNo]) REFERENCES Campsite([SiteNo]) ON DELETE CASCADE,
    FOREIGN KEY ([ESSN]) REFERENCES Employee([ESSN]) ON DELETE CASCADE
);
