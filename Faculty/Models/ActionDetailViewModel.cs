using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class ActionDetailViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Controller")]
        public string ControllerName { get; set; }

        [Display(Name = "Action")]
        public string ActionName { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime Date { get; set; }
    }
}