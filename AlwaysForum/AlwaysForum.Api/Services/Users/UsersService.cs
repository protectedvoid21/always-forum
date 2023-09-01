using AlwaysForum.Api.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.Users;

public class UsersService : IUsersService
{
    private readonly UserManager<ForumUser> _userManager;

    public UsersService(UserManager<ForumUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ForumUser> GetAsync(string id)
    {
        return await _userManager.Users
            .Where(u => u.Id == id)
            .FirstAsync();
    }

    public async Task ChangeProfilePicture(string userId, IFormFile profilePicture)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/userpictures", userId);
        filePath += Path.GetExtension(profilePicture.FileName);

        await using FileStream fileStream = new(filePath, FileMode.Create);
        await profilePicture.CopyToAsync(fileStream);

        var user = await _userManager.FindByIdAsync(userId);
        user.ProfilePicture = Path.GetFileName(filePath);
        await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> AddAsync(string userName, string password, string email)
    {
        ForumUser user = new()
        {
            Email = email,
            CreatedDate = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, password);
        return result;
    }
}