using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeVM
    {
        public string SubjectName { get; set; } = null!;
        public GradeScale GradeValue { get; set; }
    }
}
