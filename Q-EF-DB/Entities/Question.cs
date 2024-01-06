using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q_EF_DB.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? User { get; set; }
        public string? Value { get; set; }
        public int? Votes { get; set; }

        public virtual ICollection<Answer>? Answers { get; set; }
        public virtual ICollection<Tag>? Tags{ get; set; }

    }
}
