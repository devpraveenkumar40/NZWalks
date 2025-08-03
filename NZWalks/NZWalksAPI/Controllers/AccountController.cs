using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Dtos.Account;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!createdUser.Succeeded)
            {
                return StatusCode(500, createdUser.Errors);
            }

            if (registerDto.Roles != null && registerDto.Roles.Any())
            {
                var roleAddedResult = await _userManager.AddToRolesAsync(appUser, registerDto.Roles);

                if (!roleAddedResult.Succeeded) 
                {
                    return StatusCode(500, roleAddedResult.Errors);
                }

                return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser, registerDto.Roles.ToList())
                    }
                );
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName ==  loginDto.Username.ToLower());
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenService.CreateToken(user, roles.ToList());
                        return Ok(
                        new LoginResponseDto
                        {
                            Username = user.UserName,
                            Email = user.Email,
                            Token = jwtToken
                        }
                     );

                    }
                }
            }
            return BadRequest("Username or password incorrect!");
        }
    }
}
