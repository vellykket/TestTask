using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;
using TestTask.ViewModels;
using Task = TestTask.Models.Task;

namespace TestTask.Controllers
{
    
    public class CalendarController : Controller
    {
        private readonly TestTaskContext _context;

        public CalendarController(TestTaskContext context)
        {
            _context = context;
        }
        
        // GET
        [Route("Calendar/ShowMonth/{year:int?}/{month:int?}")]
        public IActionResult ShowMonth(int? year, int? month, CalendarModel calendarModel)
        {
            year ??= DateTime.Now.Year;
            if (month == null || month >= 12) month = DateTime.Now.Month;
            calendarModel.FirstDayOfTheMonth = new DateTime((int) year, (int) month, 1);
            calendarModel.StartDate = calendarModel.FirstDayOfTheMonth.AddDays(-(int)calendarModel.FirstDayOfTheMonth.DayOfWeek + 1);
            calendarModel.Tasks = _context.Tasks;
            return View(calendarModel);
        }
        
        public  IActionResult ShowDay(string dayDate, DayModel dayModel)
        {
            dayModel.Tasks = _context.Tasks.Where(task => task.Date.Date == DateTime.Parse(dayDate).Date);
            dayModel.CurrentDate = dayDate;
            var dateTimeArr = GetTimeIntervals(dayDate);
            ViewData["TimeIntervals"] = new SelectList(dateTimeArr);
            return View(dayModel);
        }

        public async Task<IActionResult> CreateTask(DayModel dayModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ShowDay", new {dayDate = dayModel.CurrentDate});
            var time = dayModel.Date;
            var date = DateTime.Parse(dayModel.CurrentDate);
            dayModel.Date = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            var task = new Task
            {
                Text = dayModel.Text,
                Date = dayModel.Date
            };
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowDay", new {dayDate = dayModel.Date.ToString("dd-MM-yyyy")});
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            var task = _context.Tasks.FirstOrDefault(task1 => task1.Id == id);
            if (task == null) return NotFound();
            var date = task.Date.ToString("dd-MM-yyyy");
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowDay", new {dayDate = date});
        }
        
        public List<string> GetTimeIntervals(string currentDay)
        {
            var array = new List<string>();
            for (var i = 0; i < 48; i++)
            {
                array.Add( DateTime.Parse(currentDay).AddMinutes(30 * i).ToShortTimeString());
            }

            return array;
        }
        
    }
}