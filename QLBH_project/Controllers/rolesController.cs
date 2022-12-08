using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;

namespace QLBH_project.Controllers
{
    public class rolesController : Controller
    {
        private readonly IRoleRepositories _role;

        public rolesController(IRoleRepositories role)
        {
            _role = role;
        }

        // GET: roles
        public IActionResult Index()
        {
            return View( _role.GetAll());
        }
        // GET: roles/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roles = _role.GetByID(id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: roles/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(roles role)
        {
            if (_role.Addroles(role))
            {
                return RedirectToAction("Index");
            }
            else return View();
        }

        // GET: roles/Edit/5
        public IActionResult Edit(Guid id)
        {

            return View(_role.GetByID(id));
        }
        [HttpPost]
        public IActionResult Edit( roles role)
        {
            if (_role.Updateroles(role))
            {
                return RedirectToAction("Index");
            }
            else return BadRequest();
        }


        public IActionResult Delete(roles role)
        {
            if (_role.Removeroles(role))
            {
                return RedirectToAction("Index");
            }

           else return BadRequest();
        }
    }
}
