using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            try
            {
                Student? filteredStudent = DbContext.Users.OfType<Student>().FirstOrDefault(filterPredicate);
                if (filteredStudent == null)
                    throw new InvalidOperationException($"{filterPredicate} returns no student");

                return Mapper.Map<StudentVm>(filteredStudent);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>filterPredicate)
        {
            try
            {
                IEnumerable<Student>? filteredStudents = DbContext.Users.OfType<Student>().Where(filterPredicate ?? (s => true));
                if (filteredStudents.IsNullOrEmpty())
                    throw new InvalidOperationException($"{filterPredicate} returns no students");

                return Mapper.Map<IEnumerable<StudentVm>>(filteredStudents);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
