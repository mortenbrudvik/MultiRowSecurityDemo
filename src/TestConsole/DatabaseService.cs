
namespace TestConsole;

public class DatabaseService(string connectionString, string dbName)
{
    public bool InitDatabase()
    {
        var database = new DatabaseManager(connectionString, dbName);
        
        if(database.Exist())
        {
            Console.WriteLine($"Database {dbName} already exists");
            
            database.Delete();
        }
        
        database.Create();
        
        database.ExecuteSqlScript(@".\Tables\Customers.sql");
        database.ExecuteSqlScript(@".\Tables\Users.sql");
        database.ExecuteSqlScript(@".\Tables\Tasks.sql");
        
        // database.ExecuteSqlScript(@".\Tables\CreateSecuritySchema.sql");
        // database.ExecuteSqlScript(@".\Tables\CreateSecurityFunction.sql");
        // database.ExecuteSqlScript(@".\Tables\CreateSecurityPolicy.sql");
        
        database.ExecuteSqlScript(@".\Tables\SeedDatabase.sql");

        
        
        // -- Setting the session context to a particular user's UserId
        //     EXEC sp_set_session_context 'UserId', 1;
        //
        // -- Testing access (should return only tasks belonging to the customer linked to UserId = 1)
        // SELECT * FROM dbo.Tasks;
        
        return true;
    }
    
    public List<Task> GetAllTasks()
    {
        var taskRepository = new TaskRepository($"{connectionString};Database={dbName}");
        return taskRepository.GetAllTasks();
    }
    
}