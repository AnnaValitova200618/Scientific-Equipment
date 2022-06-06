using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.DTO
{
    [Table("scientists")]
    public class Scientists: BaseDTO
    {

        [Column("Firstname")]
        public string firstname { get; set; }

        [Column("Patronymic")]
        public string patronymic { get; set; }

        [Column("Lastname")]
        public string lastname { get; set; }

        [Column("Login")]
        public string login { get; set; }

        [Column("Password")]
        public string password { get; set; }
        
        public Position position { get; set; }

    }
}
