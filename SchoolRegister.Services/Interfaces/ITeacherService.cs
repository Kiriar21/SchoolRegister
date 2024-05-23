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
    public interface ITeacherService
    {
        TeacherVM GetTeacher(Expression<Func<Teacher, bool>> filterPredicate);
        IEnumerable<TeacherVM> GetTeachers(Expression<Func<Teacher, bool>> ?filterPredicate = null);
        IEnumerable<GroupVm> GetTeachersGroup(TeachersGroupsVM getTeachersGroups);
    }
}
