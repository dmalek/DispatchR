using DispatchR;
using Microsoft.AspNetCore.Mvc;
using sample.Application.User.Get;
using sample.Application.User.Update;

namespace sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IDispatcher _dispatcher;

        public UserController(
            ILogger<UserController> logger,
            IDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpPost, Route("GetUser")]
        public async Task<ActionResult<GetUserResponse>> GetUser()
        {
            return await _dispatcher.SendAsync(new GetUser()
            {
                Id = Guid.NewGuid().ToString(),
            });
        }

        [HttpPost, Route("UserUpdate")]
        public async Task<ActionResult<bool>> UserUpdate(UserUpdate userUpdate)
        {
            var f = await _dispatcher.SendAsync(userUpdate);
            return f;
        }

    }
}