namespace TestConsole;
using System.Collections.Generic;
using System.Data.SqlClient;

public class TaskRepository(string connectionString)
{
    public List<Task> GetAllTasks()
    {
        var tasks = new List<Task>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT TaskId, Description, CustomerId FROM Tasks", connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            tasks.Add(new Task
            {
                TaskId = reader.GetInt32(0),
                Description = reader.GetString(1),
                CustomerId = reader.GetInt32(2)
            });
        }

        return tasks;
    }
}
