using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.DTO
{
    [Table("equipment")]
    public class Equipment: BaseDTO
    {
        [Column("Name")]
        public string name { get; set; }

        [Column("Dimensions")]
        public string dimensions { get; set; }

        [Column("Weight")]
        public decimal weight { get; set; }

        [Column("ID_responsible")]
        public int id_responsible { get; set; }

        public Scientists Scientists { get; set; }

        public string Status { get; set; }
    }
}
