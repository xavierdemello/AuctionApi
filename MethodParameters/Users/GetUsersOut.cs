using AuctionApi.Models;

namespace AuctionApi.MethodParameters.Users
{
    public class GetUsersOut : BaseMethodOut
    {
        public List<User> Users { get; set; }
    }
}
