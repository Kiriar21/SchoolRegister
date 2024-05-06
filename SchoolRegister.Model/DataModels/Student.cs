using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public virtual Group? Group { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual Parent? Parent { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual IList<Grade>? Grades { get; set; }
        [NotMapped]
        public virtual double AverageGrade
        {
            get
            {
                if(AverageGradePerSubject == null || !AverageGradePerSubject.Any())
                {
                    return 0.00;
                }
                else
                {
                    return AverageGradePerSubject.Average(x => x.Value);
                }
            }
        }
        [NotMapped]
        public virtual IDictionary<string, double> AverageGradePerSubject
        {
            get
            {
                if (GradesPerSubject == null || !GradesPerSubject.Any())
                {
                    return new Dictionary<string, double>();
                }

                return GradesPerSubject
                        .GroupBy(x => x.Key)
                        .ToDictionary(x => x.Key, y => y.Average(z => Convert.ToInt32(z.Value)));
            }
        }
        [NotMapped]
        public virtual IDictionary<string, List<GradeScale>> GradesPerSubject
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
