using Microsoft.AspNetCore.Mvc;
using Music_School.Data;
using Music_School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_School.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.ApplicationType;
            return View(objList);
        }
        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            _db.ApplicationType.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
