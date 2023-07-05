using AspNewsAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNewsAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            IConfiguration configuration, 
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials) 
        {
            var user = new IdentityUser { UserName = userCredentials.Email,
            Email = userCredentials.Email};
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if(result.Succeeded)
            {
                return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded) 
            { 
                return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Ingreso incorrecto");
            }

        }
        
        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email),
            };
            var claim = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["claimJwt"]));
            var creds = new SigningCredentials(claim, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}
