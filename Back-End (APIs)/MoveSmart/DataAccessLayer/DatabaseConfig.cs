using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public static class DatabaseConfig
    {
        private static string _connectionString;

        public static void Intialize(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static string ConnectionString => _connectionString;
    }
}
