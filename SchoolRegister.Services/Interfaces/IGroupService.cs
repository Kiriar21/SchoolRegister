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
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVM addOrUpdateGroupVM);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentTOGroupVm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectToGroupVm attachDetachSubjectGroupVm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachDetachStudentToGroupVm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectToGroupVm detachDetachSubjectGroupVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachDetachSubjectToTeacherVm);
        GroupVm GetGroup (Expression<Func<Group, bool>> filterPredicate);
        IEnumerable<GroupVm> GetGroups (Expression<Func<Group, bool>> ?filterPredicate = null);
    }
}
