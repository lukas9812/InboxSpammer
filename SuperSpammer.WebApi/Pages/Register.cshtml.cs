using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSpammer.Common;
using SuperSpammer.Storage.Infrastructure;

namespace SuperSpammer.WebApi.Pages;

public class RegisterModel : PageModel
{
    

    public RegisterModel(IUserRepository userRepository, IPasswordHasher<UserDto> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }
    public class InputModel
    {
        [Required, Display(Name = "Display name")]
        public string? DisplayName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Check if email already exists
        var existing = await _userRepository.GetByUsername(Input.Email);
        if (existing != null)
        {
            ModelState.AddModelError(string.Empty, "Email is already registered.");
            return Page();
        }

        // Create DTO and hash password
        var user = new UserDto
        {
            Email = Input.Email,
            DisplayName = Input.DisplayName
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, Input.Password);

        await _userRepository.CreateAsync(user);

        // Optionally: set session or sign-in cookie here
        // Redirect to login page
        return RedirectToPage("/Login", new { registered = true });
    }
    
    readonly IUserRepository _userRepository;
    readonly IPasswordHasher<UserDto> _passwordHasher;
}