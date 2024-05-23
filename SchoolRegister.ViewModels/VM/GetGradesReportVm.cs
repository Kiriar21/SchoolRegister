using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class GetGradesReportVm
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int GetterUserId { get; set; }
        public GetGradesReportVm() { }
        public GetGradesReportVm(int studentId, int getterUserId)
        {
            StudentId = studentId;
            GetterUserId = getterUserId;
        }
    }
}
