using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("tb_schedules")]
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_schedule { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("donor")]
        public int id_donor { get; set; }
        public virtual Donor donor { get; set; }

        [ForeignKey("bloodCenter")]
        public int id_blood_center { get; set; }
        public virtual BloodCenter bloodCenter { get; set; }
    }
}
