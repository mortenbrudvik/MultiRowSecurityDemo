IF NOT EXISTS (SELECT *
               FROM sys.schemas
               WHERE name = 'Security')
    BEGIN
        EXEC ('CREATE SCHEMA Security AUTHORIZATION dbo')
    END