using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual Teacher ?Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int ?TeacherId { get; set; }
        public virtual IList<SubjectGroup> ?SubjectGroups { get; set; }
        public virtual IList<Grade> ?Grades { get; set; }
        public Subject()
        {

        }
    }
}
