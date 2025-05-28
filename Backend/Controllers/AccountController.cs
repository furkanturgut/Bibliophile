using Backend.Dtos.AccountDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Backend.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager ; 
        private readonly ITokenService _tokenService ;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, IAccountService accountService)
        {
           this._userManager= userManager;
           this._tokenService=tokenService;
            this._signInManager = signInManager;
            this._accountService = accountService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register([FromBody]RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = new AppUser
                {
                    Name=registerDto.Name,
                    Surname= registerDto.Surname,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                
                var createUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, "User");
                    if (addRole.Succeeded)
                    {
                        return Ok(new UserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        // Hata detaylarını özel bir formatta döndür
                        var errors = addRole.Errors.Select(e => new { Code = e.Code, Description = e.Description }).ToList();
                        return StatusCode(500, new { Message = "Failed to add role", Errors = errors });
                    }
                }
                else
                {
                    // Hata detaylarını özel bir formatta döndür
                    var errors = createUser.Errors.Select(e => new { Code = e.Code, Description = e.Description }).ToList();
                    return StatusCode(500, new { Message = "Failed to create user", Errors = errors });
                }
            }
            catch (Exception e)
            {
                // Exception'ı güvenli bir şekilde döndür
                var InnerException = e.GetBaseException().Message;
                return StatusCode(500, InnerException);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login ([FromBody] LoginDto loginDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = await _userManager.Users.FirstOrDefaultAsync(email=> email.Email == loginDto.Email);
            if (appUser == null)
            {
                return Unauthorized("Email or/and password wrong");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Email or/and password wrong");
            }
            return Ok(new UserDto
            {
                UserName= appUser.UserName,
                Email= appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers ()
        {
            try
            {
                var users = await _accountService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}