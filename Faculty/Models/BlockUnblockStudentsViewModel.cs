using System.Collections.Generic;

namespace Faculty.Models
{
    public class BlockUnblockStudentsViewModel
    {
        public IEnumerable<UserViewModel> UnblockStudents { get; set; }
        public IEnumerable<UserViewModel> BlockStudents { get; set; }
    }
}