CREATE TABLE [dbo].[EmployeeTaskAssigned]
( 
	[ESSN] INT NOT NULL, 
    [TaskId] INT NOT NULL,
	PRIMARY KEY ([ESSN], [TaskId]),
	FOREIGN KEY ([ESSN], [TaskId]) REFERENCES EmployeeTasks([ESSN], [TaskId]) ON DELETE CASCADE
);
