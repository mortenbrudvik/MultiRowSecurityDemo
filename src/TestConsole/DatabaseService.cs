
namespace TestConsole;

public class DatabaseService
{
    public bool InitDatbase(string dbName)
    {
        var connectionString = "Server=localhost\\SQLEXPRESS;Integrated Security=True";
        var database = new DatabaseManager(connectionString, dbName);
        
        if(database.Exist())
        {
            Console.WriteLine($"Database {dbName} already exists");
            
            database.Delete();
        }
        
        database.Create();
        
        database.CreateTableFromFile("Customers.sql");
        database.CreateTableFromFile("Tasks.sql");

        return true;
    }
}