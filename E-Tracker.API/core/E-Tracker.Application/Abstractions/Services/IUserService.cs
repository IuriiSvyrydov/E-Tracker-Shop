using E_Tracker.Application.DTOs.User;

namespace E_Tracker.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser model);
}