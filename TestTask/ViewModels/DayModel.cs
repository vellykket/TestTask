using System;
using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.ViewModels
{
    public class DayModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public string CurrentDate { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}