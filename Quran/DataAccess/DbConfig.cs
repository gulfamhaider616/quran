namespace Quran.DataAccess
{
    /// <summary>
    /// Holds the database connection string for the (non-DI) data-access layer.
    /// Populated once at startup in Program.cs. Replaces ConfigurationManager,
    /// which is not available in ASP.NET Core.
    /// </summary>
    public static class DbConfig
    {
        public static string ConnectionString { get; set; }
    }
}
