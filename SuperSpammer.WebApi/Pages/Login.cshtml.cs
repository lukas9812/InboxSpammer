using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SuperSpammer.WebApi.Pages;

public class Login : PageModel
{
    [BindProperty]
    public LoginInput Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
        // Optional: clear old session/cookie
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        // Simple demo authentication
        if (Input.Email == "admin@example.com" && Input.Password == "Password123")
        {
            // You can store session data or authentication cookie here
            //HttpContext.Session.SetString("UserEmail", Input.Email);
            
            
            
            return RedirectToPage("/Index");
        }

        ErrorMessage = "Invalid email or password.";
        return Page();
    }
}

public class LoginInput
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}