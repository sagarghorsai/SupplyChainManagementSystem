using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DataAccessLibrary
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;
        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<UsersModel?> GetUserByUsername(string username)
        {
            var result = await _db.LoadData<UsersModel, dynamic>(
                "SELECT * FROM Users WHERE user_name = @UserName",
                new { UserName = username }
            );
            return result.FirstOrDefault();
        }

        public async Task RegisterUser(UsersModel user)
        {
            if (string.IsNullOrWhiteSpace(user.user_Name))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.");
            }

            // Hash the password before saving
            user.user_Password = BCrypt.Net.BCrypt.HashPassword(user.user_Password);

            string sql = @"INSERT INTO Users 
                           (user_first, user_last, user_name, email, user_Password, role, created_at)  
                           VALUES (@user_First, @user_Last, @user_Name, @Email, @user_Password, @Role, @CreatedAt);";

            await _db.SaveData(sql, user);
        }
    }
}
