using AuctionApi.MethodParameters.Users;
using AuctionApi.Models;

namespace AuctionApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<GetUsersOut> GetUsers(GetUsersIn input);

        Task<HiringOut> HiringUser(HiringIn input);

    }
}
