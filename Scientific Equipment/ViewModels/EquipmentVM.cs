using Scientific_Equipment.DTO;
using Scientific_Equipment.Model;
using Scientific_Equipment.MoreWindows;
using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.ViewModels
{
    class EquipmentVM: BaseVM
    {
        public CommandVM OpenTest { get; set; }
        public CommandVM OpenScientists { get; set; }
        public CommandVM OpenBooking { get; set; }

        private List<Equipment> equipments;
        public List<Equipment> Equipments
        {
            get => equipments;
            set
            {
                equipments = value;
                Signal();
            }
        }

        public EquipmentVM()
        {
            var db = SqlModel.GetInstance();
            Equipments = SqlModel.GetInstance().ListEquipments();
            


            OpenBooking = new CommandVM(() => { new Booking().Show(); });
            OpenTest = new CommandVM(() => { new Test().Show(); });
            OpenScientists = new CommandVM(() => { new ScientistsWindow().Show(); });
        }

    }
}
