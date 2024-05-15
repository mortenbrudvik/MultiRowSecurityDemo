CREATE TABLE Notes
(
    NoteId     INT PRIMARY KEY IDENTITY,
    Content    NVARCHAR(MAX),
    CustomerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId)
);
