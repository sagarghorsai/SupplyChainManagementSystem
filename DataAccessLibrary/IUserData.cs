using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IUserData
    {
        Task<bool> EmailExists(string email);
        Task<UsersModel> Login(string email, string password);
        Task Register(UsersModel user);
    }
}