using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPGProject.Dtos.User;
using RPGProject.Services.AuthService;

namespace RPGProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Allows a new player to register.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(
                new User { Username = request.Username, Email = request.Email },
                request.Password
            );
            if(!response.Success) {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Allows a player to login.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request.Username, request.Password);
            if(!response.Success) {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}