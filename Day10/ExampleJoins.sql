
SELECT 
    c.CustomerName,
    s.SegmentName
FROM Customer c
INNER JOIN Segment s
    ON c.SegmentId = s.SegmentId;

    SELECT 
    c.CustomerName,
    cp.Name AS ContactPersonName,
    cp.Title
FROM Customer c
INNER JOIN ContactPerson cp
    ON c.CustomerId = cp.CustomerId;


    SELECT * 
FROM Customer
WHERE CustomerName LIKE '%Tanaya%' OR Email LIKE '%revature.com%';


SELECT s.SegmentName, COUNT(c.CustomerId) AS CustomerCount
FROM Customer c
INNER JOIN Segment s ON c.SegmentId = s.SegmentId
GROUP BY s.SegmentName;


SELECT c.CustomerName, a.Street, a.City, a.State, a.Country
FROM Customer c
INNER JOIN CustomerAddress a
    ON c.CustomerId = a.CustomerId;


    SELECT c.CustomerName, ci.Subject, ci.InteractionDate
FROM Customer c
INNER JOIN CustomerInteraction ci
    ON c.CustomerId = ci.CustomerId
WHERE ci.InteractionDate = (
    SELECT MAX(InteractionDate)
    FROM CustomerInteraction
    WHERE CustomerId = c.CustomerId
);
