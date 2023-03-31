using AuctionApi.MethodParameters.Users;
using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUsersOut> GetUsers(GetUsersIn input);
        Task<HiringOut> HiringUser(HiringIn input);
    }
}
