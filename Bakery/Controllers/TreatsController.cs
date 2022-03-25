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
      ViewBag.ListOfTreats = _db.Treats.ToList();
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
  }
}
