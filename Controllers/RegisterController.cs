using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Models;

namespace VehiclesWebApp.Controllers;

[AllowAnonymous]
public class RegisterController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {

        var user = new IdentityUser { UserName = registerViewModel.UserName, Email = registerViewModel.Email };

        var result = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Login");
        }

        List<string> errList = result.Errors.Select(e => e.Description).ToList();

        List<ErrorViewModel> errs = errList.ConvertAll(err => new ErrorViewModel
        {
            Description = err
        });

        return RedirectToAction("RegisterInputErrors", errs);
    }

    public IActionResult RegisterInputErrors()
    {
        return View();
    }
}
