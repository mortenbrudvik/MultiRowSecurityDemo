-- Start a transaction to ensure data integrity
BEGIN TRANSACTION;

-- Insert data into the Customers table
INSERT INTO Customers (Name)
VALUES ('Customer A');
DECLARE @CustomerAId INT = SCOPE_IDENTITY(); -- Capture the last inserted identity for Customer A

INSERT INTO Customers (Name)
VALUES ('Customer B');
DECLARE @CustomerBId INT = SCOPE_IDENTITY();
-- Capture the last inserted identity for Customer B

-- Insert data into the Users table, linking each user to a customer
INSERT INTO Users (Username, CustomerId)
VALUES ('User1A', @CustomerAId);
DECLARE @User1AId INT = SCOPE_IDENTITY(); -- Capture the last inserted identity for User1A (Optional)

INSERT INTO Users (Username, CustomerId)
VALUES ('User1B', @CustomerBId);
DECLARE @User1BId INT = SCOPE_IDENTITY();
-- Capture the last inserted identity for User1B (Optional)

-- Insert data into the Tasks table, linking tasks to customers
-- Tasks for Customer A
INSERT INTO Tasks (Description, CustomerId)
VALUES ('Task 1 for Customer A', @CustomerAId);
INSERT INTO Tasks (Description, CustomerId)
VALUES ('Task 2 for Customer A', @CustomerAId);

-- Tasks for Customer B
INSERT INTO Tasks (Description, CustomerId)
VALUES ('Task 1 for Customer B', @CustomerBId);
INSERT INTO Tasks (Description, CustomerId)
VALUES ('Task 2 for Customer B', @CustomerBId);

-- Insert data into the Notes table, linking notes to customers
-- Notes for Customer A
DECLARE @i INT = 1;
WHILE @i <= 10
    BEGIN
        INSERT INTO Notes (Content, CustomerId) VALUES (CONCAT('Note ', @i, ' for Customer A'), @CustomerAId);
        SET @i = @i + 1;
    END

-- Notes for Customer B
SET @i = 1;
WHILE @i <= 10
    BEGIN
        INSERT INTO Notes (Content, CustomerId) VALUES (CONCAT('Note ', @i, ' for Customer B'), @CustomerBId);
        SET @i = @i + 1;
    END

-- Commit the transaction to finalize changes
COMMIT TRANSACTION;

