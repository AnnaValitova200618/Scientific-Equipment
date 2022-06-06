using MySql.Data.MySqlClient;
using Scientific_Equipment.Model;
using Scientific_Equipment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            var passUser = TextPassword.Password;
            var db = MySqlDB.GetDB();
            if (db.OpenConnection())
            {
                string querystring = $"select id, idPosition, Lastname from scientists where Login = @login and Password = @pass";
                using (MySqlCommand command = new MySqlCommand(querystring, MySqlDB.GetDB().GetConnection()))
                {
                    command.Parameters.AddWithValue("login", loginUser);
                    command.Parameters.AddWithValue("pass", passUser);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Auth.User = dr.GetInt32("id");
                            Auth.Role = dr.GetInt32("idPosition");
                            Auth.UserLastName = dr.GetString("Lastname");
                        }
                    }
                }
                db.CloseConnection();
            }
            else
                return;

            switch (Auth.Role)
            {
                case 1://Администратор
                    MainWindow main = new MainWindow();
                    this.Close();
                    main.Show();
                break;
                case 2://Научный работник

                break;
                case 3://Ответственный работник
                        
                break;
                default:
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }

            

        }
    }
    
}
