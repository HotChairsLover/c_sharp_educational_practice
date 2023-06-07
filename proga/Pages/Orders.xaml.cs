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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        MainWindow mainWindow;
        //bool change = false;
        public Orders(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            FirstName.Text = mainWindow.userInfo.first_name;
            name.Text = mainWindow.userInfo.name;
            LastName.Text = mainWindow.userInfo.last_name;
            Email.Text = mainWindow.userInfo.mail;
            Phone.Text = mainWindow.userInfo.phone;
            Street.Text = mainWindow.userInfo.street;
            House.Text = mainWindow.userInfo.house;
            Apartment.Text = mainWindow.userInfo.room;
            Entrance.Text = mainWindow.userInfo.porch;
            Floor.Text = mainWindow.userInfo.floor;

            int sum_price = 0;
            int discount_price = 0;
            foreach(Order order in mainWindow.ordersBook)
            {
                sum_price += order.price * order.count;
            }
            if(DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                discount_price = sum_price / 100 * 15;
                discountPrice.Content = $"{discount_price} руб. (15%)";
            }
            else
            {
                discountPrice.Content = $"{discount_price} руб. (0%)";
            }
            allPrice.Content = sum_price.ToString() + " руб.";           
            realyPrice.Content = (sum_price-discount_price).ToString() + " руб.";
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.main);
        }

        private void OpenCheck(object sender, RoutedEventArgs e)
        {
            bool is_changed = false;
            bool is_phone_changed = false;
            bool is_address_changed = false;
            if (Email.Text.Length > 0)
            {
                if (mainWindow.CheckMail(Email.Text))
                {
                    MySqlDataReader user = mainWindow.Query(String.Format("SELECT * FROM `users` WHERE `e-mail` = '{0}';", Email.Text.ToString()));
                    if (user == null) return;
                    if (user.Read())
                    {
                        mainWindow.userInfo.mail = Email.Text;
                    }
                    else
                    {
                        MessageBox.Show("Такой e-mail не зарегистрирован", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            if (FirstName.Text.Length > 0 && FirstName.Text.Length < 40 && FirstName.Text.All(char.IsLetter))
            {
                if (name.Text.Length > 0 && name.Text.Length < 40 && name.Text.All(char.IsLetter))
                {
                    if (LastName.Text.Length > 0 && LastName.Text.Length < 40 && LastName.Text.All(char.IsLetter))
                    {
                        if(mainWindow.userInfo.first_name != FirstName.Text) mainWindow.userInfo.first_name = FirstName.Text; is_changed = true;
                        if (mainWindow.userInfo.name != name.Text) mainWindow.userInfo.name = name.Text; is_changed = true;
                        if (mainWindow.userInfo.last_name != LastName.Text) mainWindow.userInfo.last_name = LastName.Text; is_changed = true;
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
                    if(mainWindow.userInfo.phone != Phone.Text) mainWindow.userInfo.phone = Phone.Text; is_phone_changed = true;
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
                if (House.Text.Length > 0 && House.Text.Length <= 5)
                {
                    if (Apartment.Text.Length > 0 && Apartment.Text.Length <= 5 && Apartment.Text.All(char.IsDigit))
                    {
                        if (Entrance.Text.Length <= 5 && Floor.Text.Length <= 5 && Floor.Text.All(char.IsDigit) && Entrance.Text.All(char.IsDigit))
                        {
                            if (Entrance.Text.Length == 0)
                            {
                                Entrance.Text = " ";
                            }
                            if (Floor.Text.Length == 0)
                            {
                                Floor.Text = " ";
                            }
                            if(mainWindow.userInfo.street != Street.Text) mainWindow.userInfo.street = Street.Text; is_address_changed = true;
                            if (mainWindow.userInfo.house != House.Text) mainWindow.userInfo.house = House.Text; is_address_changed = true;
                            if (mainWindow.userInfo.room != Apartment.Text) mainWindow.userInfo.room = Apartment.Text; is_address_changed = true;
                            if (mainWindow.userInfo.porch != Entrance.Text) mainWindow.userInfo.porch = Entrance.Text; is_address_changed = true;
                            if (mainWindow.userInfo.floor != Floor.Text) mainWindow.userInfo.floor = Floor.Text; is_address_changed = true;
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
            foreach(Order order in mainWindow.ordersBook)
            {
                mainWindow.Query(String.Format("INSERT INTO `orders` (client, dish, count, date, address, phone, status, size) " +
                    "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}');",mainWindow.userInfo.id, order.id, order.count, DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day,
                    String.Format("Адрес доставки: ул. {0} д. {1} кв. {2} под. {3} эт. {4}",mainWindow.userInfo.street, mainWindow.userInfo.house, mainWindow.userInfo.room,
                    mainWindow.userInfo.porch, mainWindow.userInfo.floor), mainWindow.userInfo.phone, "Неготов", order.id_size));
            }
            new ToastContentBuilder()
                    .AddArgument("conversationId", 9813)
                    .AddText("#ХочуПиццу")
                    .AddText("Заказ успешно оформлен. Приятно аппетита!")
                    .Show();
            if (is_changed)
            {
                mainWindow.Query(String.Format("UPDATE `mydb`.`users`" +
                "SET `surname` = '{0}', `name` = '{1}', `patronymic` = '{2}'" +
                "WHERE `id` = '{3}';",
                mainWindow.userInfo.first_name, mainWindow.userInfo.name, mainWindow.userInfo.last_name, mainWindow.userInfo.id));              
            }
            if (is_address_changed)
            {
                mainWindow.Query($"DELETE FROM `mydb`.`user_address` " +
                    $"WHERE `id_user` = '{mainWindow.userInfo.id}' AND `street` = '{mainWindow.userInfo.street}' AND `house` = '{mainWindow.userInfo.house}' AND " +
                    $"`apartment` = '{mainWindow.userInfo.room}' AND `entrance` = '{mainWindow.userInfo.porch}' AND `floor` = '{mainWindow.userInfo.floor}'");
                mainWindow.Query(String.Format("INSERT INTO `mydb`.`user_address` " +
                    "(`id_user`, `street`, `house`, `apartment`, `entrance`, `floor`) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
                    mainWindow.userInfo.id, mainWindow.userInfo.street, mainWindow.userInfo.house, mainWindow.userInfo.room, mainWindow.userInfo.porch, mainWindow.userInfo.floor));
            }
            if (is_phone_changed)
            {
                mainWindow.Query($"DELETE FROM `mydb`.`user_phone` WHERE `user_id` = '{mainWindow.userInfo.id}' AND `phone` = '{mainWindow.userInfo.phone}'");
                mainWindow.Query(String.Format("INSERT INTO `mydb`.`user_phone` " +
                    "(`user_id`, `phone`) " +
                    "VALUES ('{0}', '{1}');",
                    mainWindow.userInfo.id, mainWindow.userInfo.phone));
            }
            mainWindow.OpenPage(MainWindow.pages.check);
        }

        private void OpenBack(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.redac_orders);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
