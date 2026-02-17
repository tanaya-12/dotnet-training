CREATE TABLE Students (
    StudentID INT,
    Name NVARCHAR(50),
    Course NVARCHAR(50)
);


INSERT INTO Students (StudentID, Name, Course) VALUES
(1, 'Tanaya', 'CSE'),
(2, NULL, 'ECE'),
(3, 'Rohan', 'ME'),
(4, '', 'CE'),
(5, NULL, 'IT');


-- Update NULL or empty names to some default value
UPDATE Students
SET Name = 'Unknown'
WHERE Name IS NULL OR Name = '';

Select* from Students

ALTER TABLE Students
ALTER COLUMN Name NVARCHAR(50) NOT NULL;

Select *from Students


