using AutoMapper;
using VendingMachine.Core.Constants;
using VendingMachine.Core.DTOs;
using VendingMachine.Core.Interfaces;
using VendingMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user, await GetRole(user));
            return userDto;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null) return BadRequest("this mail already exists");
            user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            registerDto.Role = string.Equals(registerDto.Role.Trim(), AppRoles.Seller, StringComparison.OrdinalIgnoreCase) ? AppRoles.Seller : AppRoles.Buyer;
            await _userManager.AddToRoleAsync(user, registerDto.Role!);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user, registerDto.Role);
            return userDto;
        }

        private async Task<string> GetRole(User user)
        {
            return (await _userManager.IsInRoleAsync(user, AppRoles.Seller)) ? AppRoles.Seller : AppRoles.Buyer;
        }
    }
}
