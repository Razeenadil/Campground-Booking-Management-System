CREATE TABLE [dbo].[EmployeeWorkLocation]
(
	[ESSN] INT NOT NULL, 
    [CampgroundId] INT NOT NULL 
	PRIMARY KEY ([ESSN], [CampgroundId]),
	FOREIGN KEY ([ESSN]) REFERENCES Employee([ESSN]) ON DELETE CASCADE,
	FOREIGN KEY ([CampgroundId]) REFERENCES Campground([Id]) ON DELETE CASCADE
);
