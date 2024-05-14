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

    private void SetCustomerContext(SqlConnection connection, int customerId)
    {
        const string query = "EXEC sp_set_session_context @key = N'CustomerID', @value = @CustomerID";
        connection.Execute(query, new { CustomerID = customerId });
    }
}
