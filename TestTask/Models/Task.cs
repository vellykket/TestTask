using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DisplayName("Time")]
        public DateTime Date { get; set; }
    }
}