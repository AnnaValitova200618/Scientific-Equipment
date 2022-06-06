using Scientific_Equipment.DTO;
using Scientific_Equipment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scientific_Equipment.MoreWindows
{
    /// <summary>
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking : Window
    {
        public Booking(Equipment equipment)
        {
            InitializeComponent();
            DataContext = new Equipment_schedileVM(equipment);
        }
    }
}
