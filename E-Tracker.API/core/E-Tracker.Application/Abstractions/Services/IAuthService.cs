
using E_Tracker.Application.Abstractions.Services.AuthenticationService;

namespace E_Tracker.Application.Abstractions.Services;

public interface IAuthService: IExternalAuthentication,IInternalAuthentication
{

}