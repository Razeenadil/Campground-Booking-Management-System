CREATE TABLE [dbo].[Employee]
(
	[ESSN] INT NOT NULL, 
    [Age] INT NOT NULL, 
    [Sex] BIT NOT NULL, 
    [EmployeeId] INT NOT NULL IDENTITY(150000,2), 
    [Email] VARCHAR(30) NOT NULL,
    PRIMARY KEY ([ESSN]),
	FOREIGN KEY ([Email]) REFERENCES Person([Email])
);
