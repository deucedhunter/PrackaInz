using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracaInz.Data;
using PracaInz.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Coursees
                .Include(c => c.Employee)
                    .ThenInclude(e => e.Person)
                .Include(c => c.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Coursees
                .Include(c => c.Employee)
                    .ThenInclude(e => e.Person)
                .Include(c => c.Subject)
                .SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var teachers = await _context.People.Include(p => p.Employee).Where(p => p.EmployeeID != null).ToListAsync();
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName");
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "SubjectID", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,SubjectID,EmployeeID")] Course course)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _context.Employees.Include(e => e.Person).SingleOrDefaultAsync(e => e.EmployeeID == course.EmployeeID);
                var person = await _context.People.FindAsync(teacher.Person.Id);
                var subject = await _context.Subjects.FindAsync(course.SubjectID);

                course.FullName = "[" + subject.Name + "] " + person.FullName;
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var teachers = await _context.People.Include(p => p.Employee).Where(p => p.EmployeeID != null).ToListAsync();
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName", course.EmployeeID);
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "SubjectID", "Name", course.SubjectID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Coursees.SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            var teachers = await _context.People.Include(p => p.Employee).Where(p => p.EmployeeID != null).ToListAsync();
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName");
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "SubjectID", "Name", course.SubjectID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,FullName,SubjectID,EmployeeID")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var teacher = await _context.Employees.Include(e => e.Person).SingleOrDefaultAsync(e => e.EmployeeID == course.EmployeeID);
                    var person = await _context.People.FindAsync(teacher.Person.Id);
                    var subject = await _context.Subjects.FindAsync(course.SubjectID);

                    course.FullName = "[" + subject.Name + "] " + person.FullName;
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
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
            var teachers = await _context.People.Include(p => p.Employee).Where(p => p.EmployeeID != null).ToListAsync();
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName", course.EmployeeID);
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "SubjectID", "Name", course.SubjectID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Coursees
                .Include(c => c.Employee)
                    .ThenInclude(e => e.Person)
                .Include(c => c.Subject)
                .SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Coursees.SingleOrDefaultAsync(m => m.CourseID == id);
            _context.Coursees.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Coursees.Any(e => e.CourseID == id);
        }
    }
}
