using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracaInz.Data;
using PracaInz.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Controllers
{
    public class PresencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presence
                .Include(p => p.Course)
                .Include(p => p.Student)
                    .ThenInclude(s => s.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presence
                .Include(p => p.Course)
                .Include(p => p.Student)
                    .ThenInclude(s => s.Person)
                .SingleOrDefaultAsync(m => m.PresenceID == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // GET: Presences/Create
        public async Task<IActionResult> Create()
        {
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();

            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName");
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName");
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName");
            return View();
        }

        // POST: Presences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresenceID,Data,Godzina,IsPresent,StudentID,EmployeeID,CourseID")] Presence presence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();

            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName", presence.CourseID);
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName", presence.EmployeeID);
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName", presence.StudentID);
            return View(presence);
        }

        // GET: Presences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presence.SingleOrDefaultAsync(m => m.PresenceID == id);
            if (presence == null)
            {
                return NotFound();
            }
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();

            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName", presence.CourseID);
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName", presence.EmployeeID);
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName", presence.StudentID);
            return View(presence);
        }

        // POST: Presences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresenceID,Data,Godzina,IsPresent,StudentID,EmployeeID,CourseID")] Presence presence)
        {
            if (id != presence.PresenceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenceExists(presence.PresenceID))
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
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();

            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName", presence.CourseID);
            ViewData["EmployeeID"] = new SelectList(teachers, "EmployeeID", "FullName", presence.EmployeeID);
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName", presence.StudentID);
            return View(presence);
        }

        // GET: Presences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presence
                .Include(p => p.Course)
                .Include(p => p.Student)
                .Include(p => p.Student.Person)
                .SingleOrDefaultAsync(m => m.PresenceID == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // POST: Presences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presence = await _context.Presence.SingleOrDefaultAsync(m => m.PresenceID == id);
            _context.Presence.Remove(presence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenceExists(int id)
        {
            return _context.Presence.Any(e => e.PresenceID == id);
        }
    }
}
