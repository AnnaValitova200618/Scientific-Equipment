using Scientific_Equipment.DTO;
using Scientific_Equipment.Model;
using Scientific_Equipment.MoreWindows;
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
    class EquipmentVM: BaseVM
    {
        public CommandVM OpenTest { get; set; }
        public CommandVM OpenScientists { get; set; }
        public CommandVM OpenBooking { get; set; }

        private ObservableCollection<Equipment> equipments;
        public ObservableCollection<Equipment> Equipments
        {
            get => equipments;
            set
            {
                equipments = value;
                Signal();
            }
        }

        public Equipment SelectedEquipment { get; set; }

        public EquipmentVM()
        {
            var db = SqlModel.GetInstance();
            LoadEquipments();

            OpenBooking = new CommandVM(() =>
            {
                if (SelectedEquipment == null)
                {
                    MessageBox.Show("Не выбрано оборудование");
                    return;
                }
                new Booking(SelectedEquipment).ShowDialog();
                LoadEquipments();

            });
            OpenTest = new CommandVM(() => { new Test().Show(); });
            OpenScientists = new CommandVM(() => { new ScientistsWindow().Show(); });
        }

        private void LoadEquipments()
        {
            Equipments = new ObservableCollection<Equipment>(SqlModel.GetInstance().ListEquipments());
        }
    }
}
