namespace GardenService.Configs
{
    public class AuthenticationConfiguration
    {
        public JwtBearerConfiguration JwtBearerConfiguration { get; set; } = new JwtBearerConfiguration();

    }

    public class JwtBearerConfiguration
    {
        public string? Authority { get; set; }
        public string? IssuerSigningKey { get; set; }
        public TokenValidationConfiguration TokenValidationConfiguration { get; set; } =
            new TokenValidationConfiguration();
    }

    public  class TokenValidationConfiguration
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }

}
