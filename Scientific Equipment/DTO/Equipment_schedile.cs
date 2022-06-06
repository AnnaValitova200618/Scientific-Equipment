using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.DTO
{
    [Table("equipment_schedile")]
    class Equipment_schedile: BaseDTO
    {
        [Column("ID_equipment")]
        public int id_equipment { get; set; }

        [Column("ID_scientists")]
        public int id_scientists { get; set; }

        [Column("Date_start")]
        public DateTime date_start { get; set; }

        [Column("Date_end")]
        public DateTime date_end { get; set; }

        public Equipment Equipment { get; set; }

        public Scientists ScientistsBooking { get; set; }
    }
}