using TestConsole;

Console.WriteLine("Creating database");

const string connectionString = "Server=localhost\\SQLEXPRESS;Integrated Security=True";
var databaseService = new DatabaseService(connectionString, "MultiRowLevelSecurityDemo");
databaseService.InitDatabase();

var tasks = databaseService.GetAllTasks();

Console.WriteLine("Tasks:");
foreach (var task in tasks)
{
    Console.WriteLine($"TaskId: {task.TaskId}, Description: {task.Description}, CustomerId: {task.CustomerId}");
}

Console.WriteLine("Press Enter to continue...");

Console.ReadLine();



