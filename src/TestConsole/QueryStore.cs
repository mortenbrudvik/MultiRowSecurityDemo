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

        // Configure Query Store settings (optional)
        var configureQuery = $"""
                              
                                          ALTER DATABASE {databaseName} 
                                          SET QUERY_STORE ( 
                                              OPERATION_MODE = READ_WRITE, 
                                              CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), 
                                              DATA_FLUSH_INTERVAL_SECONDS = 900, 
                                              INTERVAL_LENGTH_MINUTES = 60, 
                                              MAX_STORAGE_SIZE_MB = 1000, 
                                              QUERY_CAPTURE_MODE = AUTO 
                                          );
                                      
                              """;
        connection.Execute(configureQuery);
    }

    public IEnumerable<QueryStoreResult> GetQueryStoreData()
    {
        using var connection = new SqlConnection(connectionString);

        var query = """
                    SELECT 
                        qsq.query_id AS QueryId,
                        qsq.last_execution_time AS LastExecutionTime,
                        qsqt.query_sql_text AS QueryText,
                        qsrs.count_executions AS ExecutionCount
                    FROM sys.query_store_query qsq
                        LEFT JOIN sys.query_store_query_text qsqt
                            ON qsq.query_text_id = qsqt.query_text_id
                    	LEFT JOIN sys.query_store_plan qsp
                    		ON qsq.query_id = qsp.query_id
                    	LEFT JOIN sys.query_store_runtime_stats qsrs
                    		ON qsp.plan_id = qsrs.plan_id
                    """;

        return connection.Query<QueryStoreResult>(query);
    }
}

public class QueryStoreResult
{
    public int QueryId { get; set; }
    public string QueryText { get; set; } = default!;
    public DateTime LastExecutionTime { get; set; }

    public int ExecutionCount { get; set; }
}