CREATE SECURITY POLICY TaskSecurityPolicy
    ADD FILTER PREDICATE Security.fnSecurityPredicate(CustomerID) ON dbo.Tasks
    WITH (STATE = ON);

-- CREATE SECURITY POLICY Security.NoteAccessPolicy
--     ADD FILTER PREDICATE Security.fn_noteAccessPredicate(CustomerId)
--         ON dbo.Notes
--     WITH (STATE = ON);