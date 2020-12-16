using System;
using System.Collections.Generic;
using Task = TestTask.Models.Task;

namespace TestTask.ViewModels
{
    public class CalendarModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public DateTime FirstDayOfTheMonth { get; set; }
        public DateTime StartDate { get; set; }
    }
}