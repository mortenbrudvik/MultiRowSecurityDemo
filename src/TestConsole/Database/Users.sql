CREATE TABLE Users
(
    UserId     INT PRIMARY KEY IDENTITY (1,1),
    Username   NVARCHAR(50),
    CustomerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId)
);