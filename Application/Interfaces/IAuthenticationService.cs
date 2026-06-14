using Application.Models.Requests;
using Application.Models;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDTO?> Login(UserLoginRequestDTO request);
    }
}
