using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateGroupVM
    {
        [Required]
        public int SetterUserId { get; set; }
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

    }
}
