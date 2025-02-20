using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class UsersData : IUsersData
    {
        private readonly ISqlDataAccess _db;
        public UsersData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<UsersModel>> GetUsers()
        {
            string sql = "select *from SCM_DB.Users";

            return _db.LoadData<UsersModel, dynamic>(sql, new { });
        }

        public Task InsertProduct(UsersModel product)
        {
            string sql = @"
            INSERT INTO SCM_DB.Product (name, description, unit_price, quantity_available) 
            VALUES (@Name, @Description, @UnitPrice, @QuantityAvailable)";

            return _db.SaveData(sql, product);
        }

    }
}
