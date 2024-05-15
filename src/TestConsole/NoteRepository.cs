using System.Data.SqlClient;
using Dapper;

namespace TestConsole;

public class NoteRepository(string connectionString)
{
    public List<Note> GetAllNotes()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        
        SetCustomerContext(connection, 1);  // Set to '1' for demonstration. Replace with actual logic as needed.
        
        return connection.Query<Note>("SELECT NoteId, Content, CustomerId FROM Notes").AsList();
    }

    private void SetCustomerContext(SqlConnection connection, int customerId)
    {
        const string query = "EXEC sp_set_session_context @key = N'CustomerID', @value = @CustomerID";
        connection.Execute(query, new { CustomerID = customerId });
    }
}
