using Scientific_Equipment.DTO;
using Scientific_Equipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.ViewModels
{
    class Equipment_schedileVM : BaseVM
    {

        public List<Equipment> Equipments { get; set; }
        public Equipment SelectedEquipment { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Equipment_schedileVM()
        {
            var db = SqlModel.GetInstance();
            Equipments = db.SelectEquipments();
            Start = DateTime.Now;
            End = DateTime.Now;
        }
            


    }
}
