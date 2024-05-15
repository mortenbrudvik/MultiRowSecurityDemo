CREATE FUNCTION Security.fnSecurityPredicate(@CustomerID INT)
    RETURNS TABLE
        WITH SCHEMABINDING
        AS
        RETURN SELECT 1 AS fn_securitypredicate_result
               WHERE @CustomerID = CAST(SESSION_CONTEXT(N'CustomerID') AS INT);
-- GO
-- 
-- CREATE FUNCTION Security.fn_noteAccessPredicate(@CustomerId INT)
--     RETURNS TABLE
--         WITH SCHEMABINDING
--         AS
--         RETURN SELECT 1 AS result
--                WHERE @CustomerId = CAST(SESSION_CONTEXT(N'CustomerId') AS INT);
-- GO
