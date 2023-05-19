using Shared.Requests.Auth;
using Shared.Responses.Auth;
using Shared.Wrapper;

namespace ApplicationClient.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IResult<RegistrationResponse>> RegisterUser(UserForRegistrationRequest request, CancellationToken cancellationToken);
        Task<IResult<AuthResponse>> Login(UserForAuthenticationRequest request, CancellationToken cancellationToken);
        Task Logout();
        Task<IResult<string>> RefreshToken(CancellationToken cancellationToken);
    }
}
