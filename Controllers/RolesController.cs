using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithCore.Models.ZenithModels;
using Microsoft.AspNetCore.Authorization;
using ZenithCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZenithCore.Models;
using ZenithSocietyCore.Controllers;
using Microsoft.AspNetCore.Identity;
using ZenithCore.Data.Seed.Users;
using Microsoft.AspNetCore.Builder;
using ZenithCore.Services;
using Microsoft.Extensions.Logging;

namespace ZenithCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            var roles = from a in _context.Roles

                        select a;
            return View(await roles.ToListAsync());
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                _context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"],
                    NormalizedName= collection["RoleName"]
                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(string RoleName)
        {
            var thisRole = _context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Roles.Remove(thisRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }






    }
}
