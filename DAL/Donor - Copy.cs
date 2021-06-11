using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("tb_donors")]
    public class Donor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }

        public string blood_type { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        [ForeignKey("city")]
        public int cep { get; set; }
        public virtual City city { get; set; }

    }
}