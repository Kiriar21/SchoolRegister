using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ?TeacherId { get; set; }
        public virtual Teacher ?Teacher { get; set; }
        public virtual IList<SubjectGroup> ?SubjectGroups { get; set; }
        public virtual IList<Grade> ?Grades { get; set; }
        public Subject()
        {

        }
    }
}
