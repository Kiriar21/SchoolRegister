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
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : base(dbContext, mapper, logger) 
        { 
            _userManager = userManager;
        }

        public TeacherVM GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException("Filter is null");

                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
                return Mapper.Map<TeacherVM>(teacherEntity);
            } catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<TeacherVM> GetTeachers(Expression<Func<Teacher, bool>>? filterPredicate = null)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException("Filter is null");

                var teacherEntity = DbContext.Users.OfType<Teacher>().AsQueryable().Where(filterPredicate);
                return Mapper.Map<IEnumerable<TeacherVM>>(teacherEntity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetTeachersGroup(TeachersGroupsVM getTeachersGroups)
        {
            if (getTeachersGroups == null)
                throw new ArgumentNullException("Teachers Groups Vm is null");

            var user = _userManager.FindByIdAsync(getTeachersGroups.GetterUserId.ToString()).Result;

            if (user == null || _userManager.IsInRoleAsync(user, "Teacher").Result || user is not Teacher)
                throw new InvalidOperationException("You are not Teacher, you cannot see teacher's groups list");

            var teachersSubjectsIds = DbContext.Subjects
                                                    .Where(s => s.TeacherId == getTeachersGroups.TeacherId)
                                                    .Select(s => s.TeacherId);
            var teachersGroupsIds = DbContext.SubjectGroups
                                                    .Where(sg => teachersSubjectsIds
                                                        .Contains(sg.SubjectId))
                                                    .Select(sg => sg.GroupId)
                                                    .ToList();
            var teachersGroupsEntities = DbContext.Groups
                                                    .Where(g => teachersGroupsIds
                                                        .Contains(g.GroupId))
                                                    .ToList();
            var teachersGroupsVms = Mapper.Map<IEnumerable<GroupVm>>(teachersGroupsEntities);
            return teachersGroupsVms;
        }

    }
}