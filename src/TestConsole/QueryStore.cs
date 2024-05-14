using System.Data.SqlClient;
using Dapper;

namespace TestConsole;

public class QueryStore(string connectionString)
{
    
    public static void EnableQueryStore(SqlConnection connection, string databaseName)
    {
        var enableQuery = $"""
                           ALTER DATABASE {databaseName}
                           SET QUERY_STORE = ON;
                           """;

        connection.Execute(enableQuery);
    }
    
    public IEnumerable<QueryStoreResult> GetQueryStoreData()
    {
        using var connection = new SqlConnection(connectionString);
        
        const string queryStoreDataQuery = """
                                           SELECT 
                                               qsqt.query_sql_text AS QueryText,
                                               qsp.plan_id AS PlanID,
                                               qsp.avg_duration AS AvgDuration,
                                               qsp.avg_cpu_time AS AvgCPUTime,
                                               qsp.avg_logical_io_reads AS AvgLogicalIOReads
                                           FROM 
                                               sys.query_store_plan qsp
                                           JOIN 
                                               sys.query_store_query_text qsqt
                                           ON 
                                               qsp.query_text_id = qsqt.query_text_id
                                           ORDER BY 
                                               qsp.avg_duration DESC;
                                           """;

        return connection.Query<QueryStoreResult>(queryStoreDataQuery);
    }
}

public class QueryStoreResult
{
    public string QueryText { get; set; }
    public int PlanID { get; set; }
    public long AvgDuration { get; set; }
    public long AvgCPUTime { get; set; }
    public long AvgLogicalIOReads { get; set; }
}