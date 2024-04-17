using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }
        public int? ParentId { get; set; }
        public virtual Parent? Parent { get; set; }
        public virtual IList<Grade>? Grades { get; set; }
        public virtual double AverageGrade
        {
            get
            {
                if (Grades == null || Grades.Any())
                {
                    return (double)0.00;
                }

                return Grades
                        .Average(x => (double)x.GradeValue);
            }
        }
        public virtual IDictionary<string, double> AverageGradePerSubject
        {
            get
            {
                if (Grades == null || !Grades.Any())
                {
                    return new Dictionary<string, double>();
                }

                return Grades
                        .GroupBy(x => x.Subject.Name)
                        .ToDictionary(x => x.Key, y => y.Average( z => (double)z.GradeValue));
            }
        }
        public virtual IDictionary<string, List<GradeScale>>? GradesPerSubject
        {
            get
            {
                if(Grades == null || !Grades.Any())
                {
                    return new Dictionary<string, List<GradeScale>>();
                }

                return Grades
                        .GroupBy(x => x.Subject.Name)
                        .ToDictionary(x => x.Key, y => y.Select(z => z.GradeValue).ToList());
            }
        }
        public Student() 
        {

        }

    }
}
