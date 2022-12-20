CREATE TABLE [dbo].[AmenityStoreProducts]
(
	[Name] VARCHAR(50) NOT NULL , 
    [Product] VARCHAR(25) NOT NULL
	PRIMARY KEY ([Name], [Product]),
    FOREIGN KEY ([Name]) REFERENCES AmenityStore([Name]) ON DELETE CASCADE
);
