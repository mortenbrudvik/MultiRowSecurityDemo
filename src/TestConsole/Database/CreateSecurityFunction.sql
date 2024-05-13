CREATE FUNCTION Security.fn_securitypredicate(@CustomerId AS INT)
    RETURNS TABLE
        WITH SCHEMABINDING
        AS
        RETURN SELECT 1 AS fn_securitypredicate_result
               WHERE EXISTS (SELECT 1
                             FROM dbo.Users
                             WHERE Users.CustomerId = @CustomerId
                               AND SESSION_CONTEXT(N'UserId') = Users.UserId);
