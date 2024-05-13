using Dapper;
using System.Data.SqlClient;

namespace TestConsole;

public class TaskRepository(string connectionString)
{
    public List<Task> GetAllTasks()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        
        SetCustomerContext(connection, 1);
        
        return connection.Query<Task>("SELECT TaskId, Description, CustomerId FROM Tasks").AsList();
    }
    
    public void SetCustomerContext(SqlConnection connection, int customerId)
    {
        using var command = new SqlCommand("EXEC sp_set_session_context @key, @value", connection);
        command.Parameters.AddWithValue("@key", "CustomerId");
        command.Parameters.AddWithValue("@value", customerId);
        command.ExecuteNonQuery();
    }
}
