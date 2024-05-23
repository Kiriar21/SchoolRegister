using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        GradeVM AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm);
        GradesReportVM GetGradesReportForStudent(GetGradesReportVm getGradesVM);
    }
}
