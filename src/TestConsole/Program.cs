using TestConsole;

Console.WriteLine("Creating database");
var databaseService = new DatabaseService();
databaseService.InitDatbase("MultiRowLevelSecurityDemo");

Console.WriteLine("Press Enter to continue...");

Console.ReadLine();



