using AnySocialNetwork.Models;
using AnySocialNetwork.Requests;
using Microsoft.AspNetCore.Identity;

namespace AnySocialNetwork.Services.Interfaces
{
    public interface IUserServices
    {
        Task<IdentityResult> CreateAsync(CreateUserRequest createUserRequest);
        Task<List<User>> GetAllAsync();
        Task<Object> Authenticate(LoginUserRequest loginUserRequest);
        Task<User> GetLoggedUser();
    }
}