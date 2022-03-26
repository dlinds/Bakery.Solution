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
  // [Authorize]
  public class TreatsController : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.Include(e => e.JoinEntities).ThenInclude(join => join.Flavor).ToList();
      ViewBag.ListOfTreats = _db.Treats.ToList();
      ViewBag.ListOfFlavors = _db.Flavors.ToList();
      ViewBag.PageTitle = "Treats";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      string message = treat.Name + " was successfully modified";
      return Json(new { Message = message });
    }

    [HttpPost]
    public ActionResult Delete(int treatId)
    {
      Treat treat = _db.Treats.FirstOrDefault(t => t.TreatId == treatId);
      _db.Treats.Remove(treat);
      _db.SaveChanges();
      string message = treat.Name + " was successfully deleted";
      return Json(new { Message = message });
    }

    [HttpPost]
    public ActionResult AddFlavor(int treatId, int flavorId)
    {
      _db.FlavorTreat.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavorId });
      _db.SaveChanges();
      // var joinEntry = _db.FlavorTreat.FirstOrDefault(t => t.TreatId == treatId && t.FlavorId == flavorId);
      return Json(new { message = "success" });
    }
    [HttpPost]
    public ActionResult RemoveFlavor(int treatId, int flavorId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorId == flavorId && entry.TreatId == treatId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      // var joinEntry = _db.FlavorTreat.FirstOrDefault(t => t.TreatId == treatId && t.FlavorId == flavorId);
      return Json(new { message = "success" });
    }
  }
}
