using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_EF_DB.Entities
{
    public class QuestionTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int? QuestionId { get; set; }

        public int? TagId { get; set; }

        //[ForeignKey("QuestionId")]
        //public virtual Question? Question { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag? Tag { get; set; }

    }
}
