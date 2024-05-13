CREATE TABLE Tasks
(
    TaskId      INT PRIMARY KEY IDENTITY(1,1),
    Title       NVARCHAR(100),
    Description NVARCHAR(255),
    CustomerId  INT,
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId)
);
