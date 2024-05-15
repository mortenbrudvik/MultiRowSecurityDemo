
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
        database.EnableQueryStore();
        
        database.ExecuteSqlScript(@".\Database\Customers.sql");
        database.ExecuteSqlScript(@".\Database\Users.sql");
        database.ExecuteSqlScript(@".\Database\Tasks.sql");
        database.ExecuteSqlScript(@".\Database\Notes.sql");
        
        database.ExecuteSqlScript(@".\Database\CreateSecuritySchema.sql");
        database.ExecuteSqlScript(@".\Database\CreateSecurityFunction.sql");
        database.ExecuteSqlScript(@".\Database\CreateSecurityPolicy.sql");
        
        database.ExecuteSqlScript(@".\Database\SeedDatabase.sql");
        
        return true;
    }
    
    public List<Task> GetTasks()
    {
        var taskRepository = new TaskRepository($"{connectionString};Database={dbName}");
        return taskRepository.GetAllTasks();
    }
    
    public List<Note> GetNotes()
    {
        var noteRepository = new NoteRepository($"{connectionString};Database={dbName}");
        return noteRepository.GetAllNotes();
    }
}