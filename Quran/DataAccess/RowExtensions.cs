using System;
using System.Collections.Generic;
using System.Linq;

namespace Quran.DataAccess
{
    /// <summary>
    /// Reads a column from a Dapper row (returned as IDictionary&lt;string, object&gt;) with the
    /// same null/DBNull and case-insensitive semantics as DataRow, so the existing BA mapping
    /// logic keeps working unchanged after the move from DataSet to Dapper.
    /// </summary>
    public static class RowExtensions
    {
        private static bool TryGet(IDictionary<string, object> row, string column, out object value)
        {
            value = null;
            if (row == null)
            {
                return false;
            }
            if (row.TryGetValue(column, out value))
            {
                return true;
            }
            // DataRow column lookup is case-insensitive; mirror that as a fallback.
            string key = row.Keys.FirstOrDefault(k => string.Equals(k, column, StringComparison.OrdinalIgnoreCase));
            if (key != null)
            {
                value = row[key];
                return true;
            }
            return false;
        }

        public static T Get<T>(this IDictionary<string, object> row, string column)
        {
            object value;
            TryGet(row, column, out value);

            if (value == null || value is DBNull)
            {
                // Mirror DataRow.Field<T>: null is fine for reference and Nullable<T> types,
                // but a non-nullable value type cannot hold null (it threw before too).
                if (typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
                {
                    throw new InvalidCastException(
                        "Column '" + column + "' is null and cannot be cast to " + typeof(T).Name + ".");
                }
                return default(T);
            }

            Type target = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            if (target.IsInstanceOfType(value))
            {
                return (T)value;
            }
            return (T)Convert.ChangeType(value, target);
        }

        /// <summary>
        /// Mirrors the legacy <c>row["Column"].ToString()</c> behaviour: empty string for
        /// DBNull / null / missing column, otherwise the value's string form.
        /// </summary>
        public static string Str(this IDictionary<string, object> row, string column)
        {
            object value;
            TryGet(row, column, out value);
            return value == null || value is DBNull ? "" : value.ToString();
        }
    }
}
