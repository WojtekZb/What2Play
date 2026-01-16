using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Services;

public class _LoginModel : PageModel
{
    private readonly UserService _userService;

    public _LoginModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string Role {  get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userService.Login(Email, Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return Page();
        }

        // 👇 remember user
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Role", user.role ?? "User");


        return RedirectToPage("/Index");
    }
}
