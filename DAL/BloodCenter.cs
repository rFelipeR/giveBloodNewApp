using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("tb_blood_centers")]
    public class BloodCenter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_bc { get; set; }

        public string name_bc { get; set; }

        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        [ForeignKey("city")]
        public int cep { get; set; }
        public virtual City city { get; set; }

    }
}