using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("myConnectionString")
                ?? throw new InvalidOperationException("Connection string 'myConnectionString' was not found.");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, DynamicParameters? parameters = null)
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();

            return await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<DataSet> ExecuteDataSetAsync(string storedProcedure, DynamicParameters? parameters = null)
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();

            var rows = (await connection.QueryAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure)).ToList();

            var dataSet = new DataSet();
            var table = new DataTable();

            if (rows.Count == 0)
            {
                dataSet.Tables.Add(table);
                return dataSet;
            }

            var firstRow = (IDictionary<string, object>)rows[0];

            foreach (var column in firstRow.Keys)
            {
                table.Columns.Add(column);
            }

            foreach (IDictionary<string, object> row in rows)
            {
                var dataRow = table.NewRow();

                foreach (var kvp in row)
                {
                    dataRow[kvp.Key] = kvp.Value ?? DBNull.Value;
                }

                table.Rows.Add(dataRow);
            }

            dataSet.Tables.Add(table);
            return dataSet;
        }
    }
}