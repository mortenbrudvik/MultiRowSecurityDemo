using System.Data.SqlClient;
using Dapper;

namespace TestConsole;

public class DatabaseManager(string connectionString, string databaseName)
{
    
    public bool Exist()
    {
        var result = false;
        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            
            using var command = new SqlCommand($"SELECT db_id(@databaseName)", connection);
            command.Parameters.AddWithValue("@databaseName", databaseName);
            result = (command.ExecuteScalar() != DBNull.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return result;
    }
    
    public bool Create()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"CREATE DATABASE [{databaseName}]";
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Database created successfully.");
            return true;
        }
        catch (SqlException ex)
        {
            Console.WriteLine("SQL Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return false;
    }
    
    public void Delete()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            // Set the database to single user mode to disconnect other users
            connection.Execute($"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            // Drop the database
            connection.Execute($"DROP DATABASE [{databaseName}]");
        }
        Console.WriteLine("Database deleted successfully.");
    }
    
    public void ExecuteSqlScript(string filePath)
    {
        try
        {
            var script = File.ReadAllText(filePath);
            
            var commands = script.Split(["GO", "go", "Go", "gO"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            Console.Out.WriteLine("Executing script: " +filePath + " - " + commands.Length + " commands found.");

            Execute(commands);
            Console.WriteLine($"Script executed successfully. File: {filePath} ({commands.Length}) ");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to execute script: " + ex.Message);
        }
    }

    private void Execute(string[] commands)
    {
        using var connection = new SqlConnection($"{connectionString};Database={databaseName}");
        connection.Open();
        
        foreach (var command in commands)
        {
            connection.Execute(command);
        }
    }

    public void EnableQueryStore()
    {
        QueryStore.EnableQueryStore(new SqlConnection(connectionString), databaseName);
    }
}