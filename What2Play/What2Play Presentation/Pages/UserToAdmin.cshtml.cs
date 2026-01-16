using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using What2Play_Logic.Services;
using What2Play_Logic.DTOs;

public class UserToAdminModel : PageModel
{
    private readonly UserService _userService;

    public UserToAdminModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public List<UserDTO> Users { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToPage("/Index");
        }

        Users = _userService.GetAllUsersWithRoles();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
            return Unauthorized();

        var actingUserId = HttpContext.Session.GetInt32("UserId").Value;

        _userService.UpdateUserRoles(Users, actingUserId);

        return RedirectToPage();
    }

}
