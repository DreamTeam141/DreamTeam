using System;

namespace Faculty.Logger.Entities
{
    public class ActionDetail
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}