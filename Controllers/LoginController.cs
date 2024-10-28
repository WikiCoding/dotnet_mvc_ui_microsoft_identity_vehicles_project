using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Models;

namespace VehiclesWebApp.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginController> _logger;

    public LoginController(SignInManager<IdentityUser> signInManager, ILogger<LoginController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, 
            isPersistent: loginViewModel.RememberMe, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            _logger.LogDebug("{user} logged in!", loginViewModel.Username);

            return RedirectToAction("Index", "Dashboard");
        }

        return RedirectToAction("AuthError", "Login");
    }

    public IActionResult ErrorUnauthorized()
    {
        return View();
    }

    public IActionResult AuthError()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
