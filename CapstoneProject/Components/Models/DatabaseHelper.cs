using Microsoft.Data.SqlClient;

namespace CapstoneProject.Components.Models
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        // Database connection variables
        private readonly string _server = "sql5.freesqldatabase.com";
        private readonly string _database = "sql5763241";
        private readonly string _user = "sql5763241";
        private readonly string _password = "G9mwqgdcsL";
        private readonly int _port = 3306;

        public DatabaseHelper()
        {
            _connectionString = $"Server={_server},{_port};Database={_database};User Id={_user};Password={_password};";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
