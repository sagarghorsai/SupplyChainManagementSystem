using Microsoft.Data.SqlClient;

namespace CapstoneProject.Components.Models
{
    public class UserAuthentication
    {
        private readonly DatabaseHelper _dbHelper;

        public UserAuthentication(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        // Method to register a user
        public bool Register(string username, string password)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                // Check if username already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (var cmd = new SqlCommand(checkUserQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int userExists = (int)cmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        return false; // Username already taken
                    }
                }

                string insertUserQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (var cmd = new SqlCommand(insertUserQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);  // Store plaintext password
                    cmd.Parameters.AddWithValue("@Role", "Customer");  // Default role
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
        }

        public bool Login(string username, string password)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                // Retrieve user from the database
                string selectUserQuery = "SELECT Password FROM Users WHERE Username = @Username";
                using (var cmd = new SqlCommand(selectUserQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        return false; // User not found
                    }

                    // Compare the entered password with the stored plaintext password
                    var storedPassword = result.ToString();
                    if (storedPassword == password)
                    {
                        // Successfully authenticated
                        return true;
                    }
                }
            }

            return false; // Incorrect password
        }
    }
}
