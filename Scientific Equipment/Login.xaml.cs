using MySql.Data.MySqlClient;
using Scientific_Equipment.Model;
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

namespace Scientific_Equipment
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginUser = TextLogin.Text;
            var passUser = TextPassword.Text;
            var db = MySqlDB.GetDB();
            bool enter = false;
            if (db.OpenConnection())
            {
                string querystring = $"select id, Login, Password from scientists where Login ='{loginUser}' and Password = '{passUser}'";
                using (MySqlCommand command = new MySqlCommand(querystring, MySqlDB.GetDB().GetConnection()))
                {
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            enter = dr.GetInt32("id") != 0;
                        }
                    }
                }
                db.CloseConnection();
            }
            if (enter)
            {
               
                MainWindow main = new MainWindow();
                this.Close();
                main.Show();

            }
            else
                MessageBox.Show("Такого аккаунта не существет!", "Аккаунта не существет!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            

        }
    }
    
}
