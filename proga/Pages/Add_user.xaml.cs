using Microsoft.Toolkit.Uwp.Notifications;
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
    /// Логика взаимодействия для Add_user.xaml
    /// </summary>
    public partial class Add_user : Page
    {
        MainWindow mainWindow;
        public Add_user(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.admin);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (Email.Text.Length > 0)
            {
                if (mainWindow.CheckMail(Email.Text))
                {
                    MySqlDataReader user = mainWindow.Query(String.Format("SELECT * FROM `users` WHERE `e-mail` = '{0}';", Email.Text.ToString()));
                    if (user == null) return;
                    if (!user.Read())
                    {
                        mainWindow.userInfo.mail = Email.Text;
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
            if (FirstName.Text.Length > 0 && FirstName.Text.All(char.IsLetter))
            {
                if (name.Text.Length > 0 && name.Text.All(char.IsLetter))
                {
                    if (LastName.Text.Length > 0 && LastName.Text.All(char.IsLetter))
                    {
                        mainWindow.userInfo.first_name = FirstName.Text;
                        mainWindow.userInfo.name = name.Text;
                        mainWindow.userInfo.last_name = LastName.Text;
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
            if (Phone.Text.Length > 0)
            {
                if (mainWindow.CheckPhone(Phone.Text))
                {
                    mainWindow.userInfo.phone = Phone.Text;
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
            if (Street.Text.Length > 0 && Street.Text.Length <= 30)
            {
                if (House.Text.Length > 0 && House.Text.Length <= 5 && House.Text.All(char.IsDigit))
                {
                    if (Room.Text.Length > 0 && Room.Text.Length <= 5 && Room.Text.All(char.IsDigit))
                    {
                        if (Porch.Text.Length <= 5 && Floor.Text.Length <= 5 && Porch.Text.All(char.IsDigit) && Floor.Text.All(char.IsDigit))
                        {
                            if (Porch.Text.Length == 0)
                            {
                                Porch.Text = " ";
                            }
                            if (Floor.Text.Length == 0)
                            {
                                Floor.Text = " ";
                            }
                            mainWindow.userInfo.street = Street.Text;
                            mainWindow.userInfo.house = House.Text;
                            mainWindow.userInfo.room = Room.Text;
                            mainWindow.userInfo.porch = Porch.Text;
                            mainWindow.userInfo.floor = Floor.Text;
                            mainWindow.userInfo.date = String.Format("{0}.{1}.{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
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
            string password = "";
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                password += (char)random.Next(0x0410, 0x44F);
            }
            mainWindow.userInfo.password = password;
            mainWindow.userInfo.role = 0;
            mainWindow.userInfo.login = mainWindow.userInfo.mail;

            mainWindow.Query(String.Format("INSERT INTO `mydb`.`users` " +
                "(`surname`, `name`, `patronymic`, `e-mail`, `role`, `username`, `password`, `date`) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');",
                mainWindow.userInfo.first_name, mainWindow.userInfo.name, mainWindow.userInfo.last_name, mainWindow.userInfo.mail,
                mainWindow.userInfo.role, mainWindow.userInfo.login, mainWindow.userInfo.password, mainWindow.userInfo.date));
            MySqlDataReader user_id_reader = mainWindow.Query($"SELECT `id` FROM `users` WHERE `username` = '{mainWindow.userInfo.login}'");
            user_id_reader.Read();
            string user_id = user_id_reader.GetValue(0).ToString();
            mainWindow.Query(String.Format("INSERT INTO `mydb`.`user_address` " +
                "(`id_user`, `street`, `house`, `apartment`, `entrance`, `floor`) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
                user_id, mainWindow.userInfo.street, mainWindow.userInfo.house, mainWindow.userInfo.room, mainWindow.userInfo.porch, mainWindow.userInfo.floor));
            mainWindow.Query(String.Format("INSERT INTO `mydb`.`user_phone` " +
                "(`user_id`, `phone`) " +
                "VALUES ('{0}', '{1}');",
                user_id, mainWindow.userInfo.phone));

            new ToastContentBuilder()
                                 .AddArgument("conversationId", 9813)
                                 .AddText("#ХочуПиццу")
                                 .AddText("Пользователь успешно добавлен!")
                                 .Show();
            mainWindow.OpenPage(MainWindow.pages.admin);
        }
    }
}
