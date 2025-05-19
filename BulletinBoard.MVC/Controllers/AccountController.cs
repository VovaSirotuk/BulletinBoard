using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login(string returnUrl = "/")
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };
        properties.Items["prompt"] = "login";
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }



    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Вийти з локальної cookie-автентифікації
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Announcement");
    }
}