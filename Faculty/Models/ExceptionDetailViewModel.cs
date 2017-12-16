using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class ExceptionDetailViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Message")]
        public string ExceptionMessage { get; set; }

        [Display(Name = "Controller")]
        public string ControllerName { get; set; }

        [Display(Name = "Action")]
        public string ActionName { get; set; }

        [Display(Name = "Stack Trace")]
        public string StackTrace { get; set; }

        public DateTime Date { get; set; }
    }
}