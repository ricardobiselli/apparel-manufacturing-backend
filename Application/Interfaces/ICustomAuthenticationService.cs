using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string> AuthenticateAsync(UserLoginRequest authenticationRequest);
    }
}
