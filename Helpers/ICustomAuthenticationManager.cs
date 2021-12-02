using GardenService.Configs;

namespace GardenService.Helpers
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(string username, string password);

        IDictionary<string, string> Tokens { get; }

    }
}
