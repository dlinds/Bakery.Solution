using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bakery.Models;
using System.Threading.Tasks;
using Bakery.ViewModels;
using System;
using System.Security.Principal;

namespace Bakery.Controllers
{
  public class AccountController : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BakeryContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public async Task<IActionResult> Index(string m = "")
    {
      if (!User.Identity.IsAuthenticated)
      {
        if (m == "LoginFail")
        {
          ViewBag.Message = "There was an issue when signing in with either your email or password. Please try again.";
        }
        ViewBag.AuthPageTitle = "Login";
        ViewBag.PageTitle = "Login";
      }
      else
      {
        var user = await _userManager.GetUserAsync(User);
        var fullName = _userManager.GetUserAsync(User).Result?.FullName;
        var email = _userManager.GetEmailAsync(user);
        var userName = _userManager.GetUserNameAsync(user);
        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        ViewBag.Phone = phoneNumber;
        ViewBag.FullName = fullName;
        ViewBag.AuthPageTitle = "Account Details";
        ViewBag.PageTitle = "Account Details";
      }

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPhoneNumber(string phoneNumber)
    {
      // @String.Format("{0:(###) ###-####}", Int64.Parse(phoneNumber));
      var user = await _userManager.GetUserAsync(User);
      user.PhoneNumber = String.Format("{0:(###) ###-####}", Int64.Parse(phoneNumber));
      Console.WriteLine("here: " + phoneNumber);
      IdentityResult result = await _userManager.UpdateAsync(user);
      return RedirectToAction("Index");
    }


    public ActionResult Register()
    {
      if (!User.Identity.IsAuthenticated)
      {
        ViewBag.AuthPageTitle = "Login";
      }
      else
      {
        ViewBag.AuthPageTitle = "Account Details";
      }
      ViewBag.PageTitle = "Register";
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      var user = new ApplicationUser { UserName = model.Email, FullName = model.FullName, Email = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return RedirectToAction("Index", new { m = "LoginFail" });
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}
