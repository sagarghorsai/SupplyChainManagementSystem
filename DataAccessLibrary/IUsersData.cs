using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IUsersData
    {
        Task<List<UsersModel>> GetUsers();
        Task InsertProduct(UsersModel product);
    }
}