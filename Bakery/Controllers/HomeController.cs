using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bakery.Controllers
{
  public class HomeController : Controller
  {
    private readonly BakeryContext _db;

    public HomeController(BakeryContext db)
    {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index()
    {
      if (!User.Identity.IsAuthenticated)
      {
        ViewBag.AuthPageTitle = "Login";
      }
      else
      {
        ViewBag.AuthPageTitle = "Account Details";
      }
      ViewBag.ListOfTreats = _db.Treats.Include(e => e.JoinEntities).ThenInclude(join => join.Flavor).ToList();
      ViewBag.ListOfFlavors = _db.Flavors.Include(e => e.JoinEntities).ThenInclude(join => join.Treat).ToList();
      ViewBag.PageTitle = "Bakery";
      return View();
    }
  }
}
