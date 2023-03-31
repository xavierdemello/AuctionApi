using AuctionApi.MethodParameters.Users;
using AuctionApi.Repositories.Interfaces;
using AuctionApi.Services.Interfaces;

namespace AuctionApi.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) { 
            this._repository = repository;
        }

        public async Task<GetUsersOut> GetUsers(GetUsersIn input)
        {
            return await this._repository.GetUsers(input);
        }

        public async Task<HiringOut> HiringUser(HiringIn input)
        {
            input.User.Password = input.Password;
            return await _repository.HiringUser(input);
        }
    }
}
