using Scientific_Equipment.DTO;
using Scientific_Equipment.Model;
using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scientific_Equipment.ViewModels
{
    class Equipment_schedileVM : BaseVM
    {
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
        public Equipment Equipment { get; }

        public CommandVM BookingCommand { get; set; }

        public ObservableCollection<Equipment_schedile> EquipmentSchediles { get; set; } 

        public Equipment_schedileVM(Equipment equipment)
        {
            var db = SqlModel.GetInstance();

            EquipmentSchediles = db.GetHistory(equipment);
            Equipment = equipment;

            BookingCommand = new CommandVM(()=> {
                if (End < Start)
                {
                    MessageBox.Show("Неправильно выбраны даты");
                    return;
                }
                Start = new DateTime(Start.Year, Start.Month, Start.Day);
                End = new DateTime(End.Year, End.Month, End.Day, 23,59,59);
                var row = new Equipment_schedile { date_start = Start, date_end = End, id_equipment = equipment.ID, id_scientists = Auth.User };
                row.ID = db.Insert(row);
                if (row.ID == 0)
                    return;
                row.Equipment = equipment;
                row.ScientistsBooking = new Scientists { ID = Auth.User, lastname = Auth.UserLastName };
                EquipmentSchediles.Add(row);
            });
        }
            


    }
}
