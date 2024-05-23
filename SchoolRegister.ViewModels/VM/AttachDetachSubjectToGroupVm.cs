using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachSubjectToGroupVm
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int SetterUserId { get; set; }
    }
}
