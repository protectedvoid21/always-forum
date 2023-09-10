using AlwaysForum.Api.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace AlwaysForum.Api.Repositories.Users;

public interface IUsersRepository
{
    Task<IdentityResult> AddAsync(string userName, string password, string email);

    Task<ForumUser> GetAsync(string id);

    Task ChangeProfilePicture(string userId, IFormFile profilePicture);
}