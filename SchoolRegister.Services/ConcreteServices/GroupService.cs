using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVM addOrUpdateGroupVM)
        {
            try
            {
                var user = DbContext.Users.FirstOrDefault(u => u.Id == addOrUpdateGroupVM.SetterUserId);
                if (user == null ||
                    !(
                    _userManager.IsInRoleAsync(user, "Admin").Result ||
                    _userManager.IsInRoleAsync(user, "Teacher").Result
                    )
                )
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }


                Group? addOrUpdateGroup;
                if (addOrUpdateGroupVM.Id == null)
                {
                    addOrUpdateGroup = new Group()
                    {
                        Name = addOrUpdateGroupVM.Name,
                    };
                    DbContext.Groups.Add(addOrUpdateGroup);
                }
                else
                {
                    addOrUpdateGroup = DbContext.Groups.FirstOrDefault(g => g.GroupId == addOrUpdateGroupVM.Id);
                    if (addOrUpdateGroup != null)
                    {
                        addOrUpdateGroup.Name = addOrUpdateGroupVM.Name;
                        DbContext.Groups.Update(addOrUpdateGroup);
                    }
                }

                DbContext.SaveChanges();
                return Mapper.Map<GroupVm>(addOrUpdateGroup);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentTOGroupVm)
        {
            try
            {
                var user = DbContext.Users.FirstOrDefault(u => u.Id == attachDetachStudentTOGroupVm.SetterUserId);
                if (user == null ||
                    !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }

                Student studentWithNewGroup = DbContext.Users.OfType<Student>().First(s => s.Id == attachDetachStudentTOGroupVm.StudentId);
                studentWithNewGroup.GroupId = attachDetachStudentTOGroupVm.GroupId;

                DbContext.SaveChanges();

                return Mapper.Map<StudentVm>(studentWithNewGroup);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectToGroupVm attachDetachSubjectGroupVm)
        {
            try
            {
                var user = DbContext.Users.FirstOrDefault(u => u.Id == attachDetachSubjectGroupVm.SetterUserId);
                if (user == null ||
                    !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }

                Group groupWithNewSubject = DbContext.Groups
                                                        .Include(g => g.Students)
                                                        .Include(g => g.SubjectGroups)
                                                        .First(g => g.GroupId == attachDetachSubjectGroupVm.GroupId);
                groupWithNewSubject.SubjectGroups.Add(
                    new SubjectGroup()
                    {
                        GroupId = attachDetachSubjectGroupVm.GroupId,
                        SubjectId = attachDetachSubjectGroupVm.SubjectId,
                    });
                DbContext.SaveChanges();

                return Mapper.Map<GroupVm>(groupWithNewSubject);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                User? user = DbContext.Users.FirstOrDefault(u => u.Id == attachDetachSubjectToTeacherVm.SetterUserId);

                if (user == null || !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }

                Subject subjectWithNewTeacher = DbContext.Subjects.First(s => s.SubjectId == attachDetachSubjectToTeacherVm.SubjectId);
                subjectWithNewTeacher.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                DbContext.Update(subjectWithNewTeacher);
                DbContext.SaveChanges();

                return Mapper.Map<SubjectVm>(subjectWithNewTeacher);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachDetachStudentToGroupVm)
        {
            try
            {
                var user = DbContext.Users.FirstOrDefault(u => u.Id == detachDetachStudentToGroupVm.SetterUserId);
                if (user == null ||
                    !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to change.");
                }

                Student studentRemovedFromGroup = DbContext.Users.OfType<Student>().First(s => s.Id == detachDetachStudentToGroupVm.StudentId);
                studentRemovedFromGroup.GroupId = studentRemovedFromGroup.GroupId = null;

                DbContext.Update(studentRemovedFromGroup);
                DbContext.SaveChanges();

                return Mapper.Map<StudentVm>(studentRemovedFromGroup);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectToGroupVm detachDetachSubjectGroupVm)
        {
            try
            {
                User? user = DbContext.Users.FirstOrDefault(u => u.Id == detachDetachSubjectGroupVm.SetterUserId);
                if (user == null ||
                    !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }

                Group groupWithSubjectRemoved = DbContext.Groups
                                                        .Include(g => g.Students)
                                                        .Include(g => g.SubjectGroups)
                                                        .First(g => g.GroupId == detachDetachSubjectGroupVm.GroupId);

                SubjectGroup subjectGroupToRemove = groupWithSubjectRemoved.SubjectGroups
                                                                                    .First(sg => sg.SubjectId == detachDetachSubjectGroupVm.SubjectId);

                groupWithSubjectRemoved.SubjectGroups.Remove(subjectGroupToRemove);

                DbContext.Update(groupWithSubjectRemoved);
                DbContext.SaveChanges();

                return Mapper.Map<GroupVm>(groupWithSubjectRemoved);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachDetachSubjectToTeacherVm)
        {
           try
            {
                User? user = DbContext.Users.FirstOrDefault(u => u.Id == detachDetachSubjectToTeacherVm.SetterUserId);

                if (user == null || !_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    throw new InvalidOperationException("The Setter user don't have permissions to read.");
                }

                var subjectWithTeacherRemoved = DbContext.Subjects.First(s => s.SubjectId == detachDetachSubjectToTeacherVm.SubjectId);
                subjectWithTeacherRemoved.TeacherId = null;

                DbContext.Update(subjectWithTeacherRemoved);
                DbContext.SaveChanges();

                return Mapper.Map<SubjectVm>(subjectWithTeacherRemoved);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException("Filter is null");

                var groupEntity = DbContext.Groups.FirstOrDefault(filterPredicate);
                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;

            } catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException("Filter is null");

                var groupEntities = DbContext.Groups.AsQueryable().Where(filterPredicate);
                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
                return groupVms;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
