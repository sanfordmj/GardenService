using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GardenService.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    [AllowAnonymous] 
    public class TokenGenController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<TokenGenController>? _logger;

        public TokenGenController(ILogger<TokenGenController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        
        [HttpPost]
        public async Task<ActionResult<string>> PostTokenGen(int IX_User)
        {
            var mySecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes("asdv234234^&%&^%&^hjsdfb2%%%"));
            var myIssuer = "http://localhost/gardenservice.signit/";
            var myAudience = "http://dockergardenservice.com/";

            var claims = new System.Security.Claims.Claim[]
            {
                new System.Security.Claims.Claim("IxUser", IX_User.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(365),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(mySecurityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}