CREATE TABLE [dbo].[Campground]
(
	[Id] INT NOT NULL IDENTITY(1,2),
	[Name] VARCHAR(50) NOT NULL,
	[TotalSites] INT NOT NULL,
	PRIMARY KEY([Id])
);
