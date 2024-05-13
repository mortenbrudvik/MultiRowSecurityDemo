CREATE SECURITY POLICY Security.CustomerAccessPolicy
    ADD FILTER PREDICATE Security.fn_securitypredicate(CustomerId)
        ON dbo.Tasks
    WITH (STATE = ON);
