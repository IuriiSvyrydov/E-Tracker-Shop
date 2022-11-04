using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDTO CreateAccessToken(int minute);
}