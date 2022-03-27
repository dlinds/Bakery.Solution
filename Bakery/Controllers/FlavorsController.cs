using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Library.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
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
      List<Flavor> model = _db.Flavors.Include(f => f.JoinEntities).ThenInclude(join => join.Treat).ToList();
      ViewBag.ListOfTreats = _db.Treats.ToList();
      ViewBag.ListOfFlavors = _db.Flavors.ToList();
      ViewBag.PageTitle = "Flavors";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      string message = flavor.Name + " was successfully modified";
      return Json(new { Message = message });
    }

    [HttpPost]
    public ActionResult Delete(int flavorId)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == flavorId);
      _db.Flavors.Remove(flavor);
      _db.SaveChanges();
      string message = flavor.Name + " was successfully deleted";
      return Json(new { Message = message });
    }
  }
}
