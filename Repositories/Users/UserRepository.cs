using AuctionApi.Data;
using AuctionApi.MethodParameters.Users;
using AuctionApi.Models;
using AuctionApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionApi.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            // Ignorar verificación del certificado SSL
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            this._context = context;
        }

        public async Task<GetUsersOut> GetUsers(GetUsersIn input)
        {
            var users =  await this._context.Users.ToListAsync();
            GetUsersOut result = new GetUsersOut
            {
                Users = users,
                OperationResult = OperationResult.Success
            };

            return result;
        }

        public async Task<HiringOut> HiringUser(HiringIn input)
        {
            HiringOut result = new HiringOut { OperationResult = OperationResult.Error_Generic };

            input.User.StatusId = UserStatus.Pending;
            this._context.Add(input.User);
            result.OperationResult = await this._context.SaveChangesAsync() > 0 ? OperationResult.Success : OperationResult.Error_Generic;

           return result;
        }
    }
}
