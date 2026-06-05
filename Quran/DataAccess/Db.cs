using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Quran.DataAccess
{
    /// <summary>
    /// Single place that owns the database connection and the Dapper call boilerplate
    /// for the whole app. The connection string is assigned once at startup (Program.cs);
    /// every DataAccess class runs its queries through the helpers here so the
    /// open-connection / map-rows pattern lives in exactly one file.
    /// </summary>
    public static class Db
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection Connection()
        {
            return new SqlConnection(ConnectionString);
        }

        // ---- Stored procedures ---------------------------------------------
        public static List<IDictionary<string, object>> QueryProc(string procedure, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Query(procedure, param, commandType: CommandType.StoredProcedure)
                                 .Cast<IDictionary<string, object>>().ToList();
            }
        }

        public static IDictionary<string, object> QueryProcSingle(string procedure, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Query(procedure, param, commandType: CommandType.StoredProcedure)
                                 .Cast<IDictionary<string, object>>().FirstOrDefault();
            }
        }

        /// <summary>For procedures that return two result sets (e.g. data + a TotalRecords count).</summary>
        public static (List<IDictionary<string, object>> First, List<IDictionary<string, object>> Second)
            QueryProcTwo(string procedure, object param = null)
        {
            using (SqlConnection connection = Connection())
            using (SqlMapper.GridReader grid = connection.QueryMultiple(procedure, param, commandType: CommandType.StoredProcedure))
            {
                var first = grid.Read().Cast<IDictionary<string, object>>().ToList();
                var second = grid.IsConsumed
                    ? new List<IDictionary<string, object>>()
                    : grid.Read().Cast<IDictionary<string, object>>().ToList();
                return (first, second);
            }
        }

        public static int ExecuteProc(string procedure, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Execute(procedure, param, commandType: CommandType.StoredProcedure);
            }
        }

        // ---- Inline (CommandType.Text) SQL ---------------------------------
        public static List<IDictionary<string, object>> Query(string sql, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Query(sql, param).Cast<IDictionary<string, object>>().ToList();
            }
        }

        public static IDictionary<string, object> QuerySingle(string sql, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Query(sql, param).Cast<IDictionary<string, object>>().FirstOrDefault();
            }
        }

        public static int Execute(string sql, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.Execute(sql, param);
            }
        }

        public static T ExecuteScalar<T>(string sql, object param = null)
        {
            using (SqlConnection connection = Connection())
            {
                return connection.ExecuteScalar<T>(sql, param);
            }
        }
    }
}
