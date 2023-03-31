using AuctionApi.MethodParameters.Users;
using AuctionApi.Models;
using AuctionApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApi.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            this._userService = userService;
        }

        [HttpGet("GetUsers", Name = "GetUsers")]
        public async Task<ActionResult<GetUsersOut>> GetUsers([FromQuery]GetUsersIn request)
        {
            var users = await _userService.GetUsers(request);

            return Ok(users);
        }


        [HttpPost("hiringUser", Name = "HiringUser")]
        public async Task<ActionResult<HiringOut>> HiringUser([FromBody] HiringIn request)
        {
            if (request == null || request.User == null)
            {
                return NotFound(new HiringOut { OperationResult = OperationResult.Error_Generic });
            }
            HiringOut hiringResult = await _userService.HiringUser(request);

            if (hiringResult.OperationResult != OperationResult.Success) return Conflict(new HiringOut { OperationResult = OperationResult.Error_Generic });

            return Ok(hiringResult);
        }
    }
}
