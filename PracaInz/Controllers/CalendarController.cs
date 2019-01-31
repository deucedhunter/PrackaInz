using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracaInz.Data;
using PracaInz.Models;
using PracaInz.Models.ViewModels;
using System;
using System.Linq;

namespace PracaInz.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new NewEventVM()
            {
                Event = new Event(),
                EventList = _context.Events.ToList(),
                Employees = _context.Employees.Include(e => e.Person).ToList(),
                Classes = _context.Classes.ToList()
            };
            ViewData["EmplyeesList"] = new SelectList(_context.People.Where(p => p.EmployeeID != null), "Id", "FullName");
            return View(vm);
        }

        public IActionResult Create(Event EventVM)
        {
            //if (ModelState.IsValid)
            //{
            //    var eventToDb = EventVM.Events.SingleOrDefault();

            //    _context.Events.Add(eventToDb);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //var teacher = EventVM.Employees.Where()
            //ViewData["EmplyeesList"] = new SelectList(_context.People.Where(p => p.EmployeeID != null), "Id", "FullName", EventVM.Events);
            return RedirectToAction("Index");
        }




        // GET: api/Calendar
        [HttpGet]
        public JsonResult GetEvents()
        {
            Event events = _context.Events.Include(e => e.Author).First();
            var callendarObj = new CallendarScript()
            {
                title = events.Author.FullName + ": " + events.Description,
                start = new DateTime(events.Date.Year, events.Date.Month, events.Date.Day, events.Time.Hour, events.Time.Minute, events.Time.Second),
                end = new DateTime(events.Date.Year, events.Date.Month, events.Date.Day, events.Time.Hour, events.Time.Minute, events.Time.Second)
            };


            return Json(callendarObj);
        }

        //// GET: api/Calendar/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEvent([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventID == id);

        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(@event);
        //}

        //// PUT: api/Calendar/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event @event)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != @event.EventID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@event).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Calendar
        //[HttpPost]
        //public async Task<IActionResult> PostEvent([FromBody] Event @event)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Events.Add(@event);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.EventID }, @event);
        //}

        //// DELETE: api/Calendar/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventID == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Events.Remove(@event);
        //    await _context.SaveChangesAsync();

        //    return Ok(@event);
        //}

        //private bool EventExists(int id)
        //{
        //    return _context.Events.Any(e => e.EventID == id);
        //}
    }
}