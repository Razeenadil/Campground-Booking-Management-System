CREATE TABLE [dbo].[CustomerBooking]
(
	[BookingId] INT NOT NULL IDENTITY(100000,6), 
    [BillingAddress] VARCHAR(50) NOT NULL, 
    [SiteNo] INT NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [CustomerEmail] VARCHAR(30) NOT NULL,
    PRIMARY KEY ([BookingId]),
	FOREIGN KEY ([SiteNo]) REFERENCES Campsite([SiteNo]) ON DELETE CASCADE,
	FOREIGN KEY ([CustomerEmail]) REFERENCES Person([Email]) ON DELETE CASCADE
);
