using Common.Models.Database;
using Common.Models.Inbound;
using Common.Models.Outbound;

namespace BusinessLogicLayer.Contracts
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(RegisterUser user);

        Task<LogedInUser> UpdateUser(UpdateUser user);

        //Task<User> DeleteUser(User user);
        //Task<User> GetUserById(Guid id);
        //Task<User> GetAll();
        Task<LogedInUser> Login(string username, string password);

        //Task<string> Verify(VerifyUserModel user);
        //Task<List<SellerView>> GetSellers();
        Task<LogedInUser> FacebookLogin(FacebookLoginUser facebookUser);
    }
}