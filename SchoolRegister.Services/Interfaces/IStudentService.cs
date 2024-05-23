using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate);
    }
}
