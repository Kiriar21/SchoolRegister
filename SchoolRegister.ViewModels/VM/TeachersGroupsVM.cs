using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class TeachersGroupsVM
    {
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int GetterUserId { get; set; }
    }
}
