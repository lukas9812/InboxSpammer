using Microsoft.AspNetCore.Identity;
using SuperSpammer.Infastructure;

namespace SuperSpammer.Common.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<UserDto> _userManager;

    public AccountService(UserManager<UserDto> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> ValidateCredentials(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            // User not found
            return false;
        }

        // Verify the entered password against the stored hash
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }
}