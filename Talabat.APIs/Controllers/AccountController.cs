using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
  
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper ,IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                DisplayName = user.DisplayName
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse(){Errors =  new []{"Email address is in use"}});
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                DisplayName = user.DisplayName
            };
        }   
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue( ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                DisplayName = user.DisplayName
            };
            
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }
        [Authorize] 
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("Problem updating the user");
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User not found"));
            }

            user.DisplayName = updateUserDto.DisplayName;
            user.PhoneNumber = updateUserDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Problem updating the user"));
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                DisplayName = user.DisplayName
            };
        }
        [Authorize]
        [HttpPut("updatepassword")]
        public async Task<ActionResult> UpdateUserPassword([FromBody] Dictionary<string, string> passwords)
        {
            if (!passwords.ContainsKey("currentPassword") || !passwords.ContainsKey("newPassword"))
            {
                return BadRequest(new ApiResponse(400, "Current and new passwords are required"));
            }

            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User not found"));
            }

            var result = await _userManager.ChangePasswordAsync(user, passwords["currentPassword"], passwords["newPassword"]);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Problem updating the password"));
            }

            return Ok();
        }
    }
}