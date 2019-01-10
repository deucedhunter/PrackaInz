using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracaInz.Data;
using PracaInz.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Controllers
{
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Grades
                .Include(g => g.Course)
                .Include(g => g.Student)
                    .ThenInclude(s => s.Person);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Course)
                .Include(g => g.Student)
                .ThenInclude(s => s.Person)
                .SingleOrDefaultAsync(m => m.GradeID == id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public async Task<IActionResult> Create()
        {
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();


            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName");
            ViewData["EmployeerID"] = new SelectList(teachers, "EmployeeID", "FullName");
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName");
            var list = GenerateGradeList();
            ViewData["GradeList"] = new SelectList(list);
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeID,Value,StudentID,EmployeerID,CourseID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();


            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName");
            ViewData["EmployeerID"] = new SelectList(teachers, "EmployeeID", "FullName");
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName");
            var list = GenerateGradeList();
            ViewData["GradeList"] = new SelectList(list, "Value", "Text", grade.Value);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.SingleOrDefaultAsync(m => m.GradeID == id);
            if (grade == null)
            {
                return NotFound();
            }

            var students = await _context.People.Where(p => p.StudentID != null).ToListAsync();
            var teachers = await _context.People.Where(p => p.EmployeeID != null && p.Employee.isTeacher).ToListAsync();


            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName", grade.CourseID);
            ViewData["EmployeerID"] = new SelectList(teachers, "EmployeeID", "FullName", grade.EmployeerID);
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName", grade.StudentID);

            var list = GenerateGradeList();
            ViewData["GradeList"] = new SelectList(list);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeID,Value,StudentID,EmployeerID,CourseID")] Grade grade)
        {
            if (id != grade.GradeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeID))
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


            ViewData["CourseID"] = new SelectList(_context.Coursees, "CourseID", "FullName", grade.CourseID);
            ViewData["EmployeerID"] = new SelectList(teachers, "EmployeeID", "FullName", grade.EmployeerID);
            ViewData["StudentID"] = new SelectList(students, "StudentID", "FullName", grade.StudentID);
            var list = GenerateGradeList();
            ViewData["GradeList"] = new SelectList(list);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Course)
                .Include(g => g.Employeer)
                .Include(g => g.Student)
                .SingleOrDefaultAsync(m => m.GradeID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.SingleOrDefaultAsync(m => m.GradeID == id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeID == id);
        }
        private List<decimal> GenerateGradeList()
        {
            List<decimal> list = new List<decimal>();
            decimal tmp = (decimal)1.5;
            for (int i = 0; i < 7; i++)
            {
                tmp += (decimal)0.5;
                list.Add(tmp);
            }

            return list;
        }
    }
}
