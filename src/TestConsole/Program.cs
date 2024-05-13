using TestConsole;

Console.WriteLine("Creating database");

const string connectionString = "Server=localhost\\SQLEXPRESS;Integrated Security=True";
var databaseService = new DatabaseService(connectionString);
databaseService.InitDatbase("MultiRowLevelSecurityDemo");


Console.WriteLine("Press Enter to continue...");

Console.ReadLine();



