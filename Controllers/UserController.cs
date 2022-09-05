using JobAdder.Users.API.Application.Repositories;
using JobAdder.Users.API.Application.Services;
using JobAdder.Users.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobAdder.Users.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(LoginModel model)
        {
            var authenticateResult = await _userRepository.AuthenticateAsync(model.UserName, model.Password);
            if (authenticateResult)
            {
                var token = _tokenService.CreateToken(model.UserName);
                return Ok(new
                {
                    UserName = model.UserName,
                    Token = token
                });
            }

            return Ok(authenticateResult);
        }
    }
}
