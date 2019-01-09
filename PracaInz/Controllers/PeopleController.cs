using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracaInz.Data;
using PracaInz.Models;
using PracaInz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(string role = null)
        {

            if (role == "Student")
            {
                return View(await _context.People
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Student).Where(p => p.StudentID != null).ToListAsync());
            }
            else if (role == "Employee")
            {
                return View(await _context.People
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Employee).Where(p => p.EmployeeID != null).ToListAsync());
            }
            return View(await _context.People
                .Include(p => p.ApplicationUser)
                .Include(p => p.Employee)
                .Include(p => p.Student).ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.ApplicationUser)
                .Include(p => p.Employee)
                .Include(p => p.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID");
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstMidName,LastName,BirthDate,Pesel,ApplicationUserID,StudentID,EmployeeID")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", person.ApplicationUserID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", person.EmployeeID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "StudentID", person.StudentID);

            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.Include(p => p.Employee).SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            Role role;
            if (person.EmployeeID != null)
            {
                if (person.Employee.isTeacher)
                {
                    role = Role.Teacher;
                }
                else
                {
                    role = Role.Other;
                }
            }
            else if (person.StudentID != null)
            {
                role = Role.Student;
            }
            else
            {
                role = new Role();
            }
            var vm = new EditPersonVM
            {
                Person = person,
                Role = role
            };

            List<SelectListItem> roleList = PopulateRoleList();

            ViewData["Roles"] = new SelectList(roleList, "Value", "Text", vm.Role);

            return View(vm);
        }

        private static List<SelectListItem> PopulateRoleList()
        {
            var roleList = new List<SelectListItem>();

            foreach (var item in Enum.GetValues(typeof(Role)))
            {
                roleList.Add(new SelectListItem { Text = Enum.GetName(typeof(Role), item), Value = item.ToString() });
            }

            return roleList;
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPersonVM personVM)
        {
            if (id != personVM.Person.Id)
            {
                return NotFound();
            }


            switch (personVM.Role.ToString())
            {
                case "Student":

                    personVM.Person.Student = new Student();
                    break;
                case "Teacher":
                    personVM.Person.Employee = new Employee();
                    personVM.Person.Employee.isTeacher = true;
                    break;
                case "Other":
                    personVM.Person.Employee = new Employee();
                    personVM.Person.Employee.isTeacher = false;
                    break;
                default:
                    throw new Exception("Coś poszło nie tak, zła rola!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.People.Update(personVM.Person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<SelectListItem> roleList = PopulateRoleList();

            ViewData["Roles"] = new SelectList(roleList, "Value", "Text", personVM.Role.Value);
            return View(personVM);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.ApplicationUser)
                .Include(p => p.Employee)
                .Include(p => p.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
