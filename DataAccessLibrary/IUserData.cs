using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IUserData
    {
        Task<UsersModel?> GetUserByUsername(string username);
    }
}