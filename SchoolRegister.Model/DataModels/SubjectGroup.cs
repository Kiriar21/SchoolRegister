using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class SubjectGroup
    {
        public virtual Subject Subject { get; set; } = null!;
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Group Group { get; set; } = null!;
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public SubjectGroup()
        { 

        }
    }
}
