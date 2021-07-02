using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("tb_schedulings")]
    public class Scheduling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("bloodCenter")]
        public int id_bc { get; set; }
        public virtual BloodCenter bloodCenter { get; set; }
        public DateTime date { get; set; }

        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool? answered { get; set; }
    }
}
