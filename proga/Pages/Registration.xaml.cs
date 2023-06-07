using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proga.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        MainWindow mainWindow;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            if(tb_mail.Text.Length > 0)
            {
                if (mainWindow.CheckMail(tb_mail.Text))
                {
                    MySqlDataReader user = mainWindow.Query(String.Format("SELECT * FROM `users` WHERE `e-mail` = '{0}';",tb_mail.Text.ToString()));
                    if (user == null) return;
                    if (!user.Read())
                    {
                        mainWindow.userInfo.mail = tb_mail.Text;
                    }
                    else
                    {
                        MessageBox.Show("Такой e-mail уже зарегистрирован", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    user.Close();
                }
                else
                {
                    MessageBox.Show("Пожалуйста укажите e-mail в формате хххххх@хххх.хх ", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста укажите e-mail ", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if(first_name.Text.Length > 0 && first_name.Text.All(char.IsLetter))
            {
                if (name.Text.Length > 0 && name.Text.All(char.IsLetter))
                {
                    if (last_name.Text.Length > 0 && last_name.Text.All(char.IsLetter))
                    {
                        mainWindow.userInfo.first_name = first_name.Text;
                        mainWindow.userInfo.name = name.Text;
                        mainWindow.userInfo.last_name = last_name.Text;
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста укажите Отчество", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста укажите Имя", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста укажите Фамилия", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if(tb_phone.Text.Length > 0)
            {
                if (mainWindow.CheckPhone(tb_phone.Text))
                {
                    mainWindow.userInfo.phone = tb_phone.Text;
                }
                else
                {
                    MessageBox.Show("Пожалуйста укажите телефон в формате +79000000000", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста укажите телефон", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (tb_street.Text.Length > 0  && tb_street.Text.Length <= 30)
            {
                if (tb_house.Text.Length > 0 && tb_house.Text.Length <= 5)
                {
                    if(tb_room.Text.Length > 0 && tb_room.Text.Length <= 5 && tb_room.Text.All(char.IsDigit))
                    {
                        if(tb_porch.Text.Length <= 5 && tb_floor.Text.Length <= 5 && tb_floor.Text.All(char.IsDigit) && tb_porch.Text.All(char.IsDigit))
                        {
                            if(tb_porch.Text.Length == 0)
                            {
                                tb_porch.Text = " ";
                            }
                            if(tb_floor.Text.Length == 0)
                            {
                                tb_floor.Text = " ";
                            }
                            mainWindow.userInfo.street = tb_street.Text;
                            mainWindow.userInfo.house = tb_house.Text;
                            mainWindow.userInfo.room = tb_room.Text;
                            mainWindow.userInfo.porch = tb_porch.Text;
                            mainWindow.userInfo.floor = tb_floor.Text;
                            mainWindow.userInfo.date = String.Format("{0}.{1}.{2}",DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
                        }
                        else
                        {
                            MessageBox.Show("Номер подьезда и этажа не должен быть длиннее 5 символов", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Номер квартиры не должен быть длиннее 5 символов", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Номер дома не должен быть длиннее 5 символов", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Название не должен быть длиннее 30 символов", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            mainWindow.OpenPage(MainWindow.pages.end_registration);
        }

        private void Ath(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите прекратить регистрацию?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                mainWindow.OpenPage(MainWindow.pages.authorization);
        }
    }
}
