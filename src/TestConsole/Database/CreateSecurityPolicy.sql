CREATE SECURITY POLICY TaskSecurityPolicy
    ADD FILTER PREDICATE dbo.fnSecurityPredicate(CustomerID) ON dbo.Tasks
    WITH (STATE = ON);
