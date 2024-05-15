CREATE FUNCTION Security.fnSecurityPredicate(@CustomerID INT)
    RETURNS TABLE
        WITH SCHEMABINDING
        AS
        RETURN SELECT 1 AS fn_securitypredicate_result
               WHERE @CustomerID = CAST(SESSION_CONTEXT(N'CustomerID') AS INT);
