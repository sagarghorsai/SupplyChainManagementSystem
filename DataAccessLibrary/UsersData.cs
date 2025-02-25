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


        public async Task<UsersModel?> GetUserByUsername(string username)
        {
            var result = await _db.LoadData<UsersModel, dynamic>(
                "SELECT * FROM Users WHERE user_name = @UserName",
                new { UserName = username }
            );
            return result.FirstOrDefault();
        }


    }
}
