CREATE SECURITY POLICY Security.TaskSecurityPolicy
    ADD FILTER PREDICATE Security.fnSecurityPredicate(CustomerID) ON dbo.Tasks
    WITH (STATE = ON);

GO

CREATE SECURITY POLICY Security.NoteAccessPolicy
    ADD FILTER PREDICATE Security.fnSecurityPredicate(CustomerID) ON dbo.Notes
    WITH (STATE = ON);