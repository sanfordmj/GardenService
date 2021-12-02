
using GardenService.Configs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace GardenService.Helpers
{
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;
        private readonly IConfiguration Configuration;

        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticationManager customAuthenticationManager, IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            this.customAuthenticationManager = customAuthenticationManager;
            Configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("JWT", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("JWT".Length).Trim();            
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return validateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private AuthenticateResult validateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var myKeyValue = Configuration["AuthenticationConfiguration:JwtBearerConfiguration:IssuerSigningKey"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(myKeyValue));

            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (securityToken == null)
                throw new NullReferenceException();

            SecurityToken validatedToken = null!;

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = Configuration["AuthenticationConfiguration:JwtBearerConfiguration:TokenValidationConfiguration:Issuer"],
                ValidAudience = Configuration["AuthenticationConfiguration:JwtBearerConfiguration:TokenValidationConfiguration:Audience"],
                IssuerSigningKey = mySecurityKey
            };

            ClaimsPrincipal principal = null!;
            try
            {   
                principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var IxUser = Convert.ToInt32(securityToken.Claims.First(claim => claim.Type == "IxUser").Value);
                
                var claims = new List<Claim>
                {
                    new Claim("IxUser", IxUser.ToString())                   
                };
                //Startup.IxUser = IxUser;

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return AuthenticateResult.Fail("Unauthorized");
            }
            
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }

}
