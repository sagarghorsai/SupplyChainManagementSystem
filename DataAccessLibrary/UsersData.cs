using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;
        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<UsersModel> Login(string email, string password)
        {
            string sql = "SELECT * FROM Users WHERE Email = @Email AND PasswordHash = @Password";

            var users = await _db.LoadData<UsersModel, dynamic>(sql, new { Email = email, Password = password });

            return users.FirstOrDefault();
        }

        public async Task Register(UsersModel user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.UserPassword))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }

            string sql = @"INSERT INTO Users (FullName, Email, PasswordHash, Role)  
                   VALUES (@FullName, @Email, @PasswordHash, @Role);";

            await _db.SaveData(sql, user);
        }

        public async Task<bool> EmailExists(string email)
        {
            string sql = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
            var count = await _db.LoadData<int, dynamic>(sql, new { Email = email });

            return count.FirstOrDefault() > 0;
        }
    }
}
