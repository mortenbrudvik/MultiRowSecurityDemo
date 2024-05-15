using TestConsole;

Console.WriteLine("Creating database");

const string connectionString = "Server=localhost\\SQLEXPRESS;Integrated Security=True";
var databaseService = new DatabaseService(connectionString, "MultiRowLevelSecurityDemo");
databaseService.InitDatabase();

var tasks = databaseService.GetTasks();

Console.WriteLine("Tasks:");
foreach (var task in tasks)
{
    Console.WriteLine($"TaskId: {task.TaskId}, Description: {task.Description}, CustomerId: {task.CustomerId}");
}

var notes = databaseService.GetNotes();

Console.WriteLine("Notes:");
foreach (var note in notes)
{
    Console.WriteLine($"NoteId: {note.NoteId}, Content: {note.Content}, CustomerId: {note.CustomerId}");
}


var queryStore = new QueryStore(connectionString + ";Database=MultiRowLevelSecurityDemo");
var queryStoreData = queryStore.GetQueryStoreData();
foreach (var query in queryStoreData)
{
    Console.WriteLine($"QueryId: {query.QueryId}, LastExecutionTime: {query.LastExecutionTime}, QueryText: {query.QueryText}, ExecutionCount: {query.ExecutionCount}");
}

Console.WriteLine("Press Enter to continue...");

Console.ReadLine();



