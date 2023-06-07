using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Toolkit.Uwp.Notifications;


namespace proga.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        MainWindow mainWindow;
        public Authorization(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            SaveData();
        }
        public void SaveData()
        {
            if (File.Exists(mainWindow.localPath + @"\save.txt"))
            {
                StreamReader sr = new StreamReader(mainWindow.localPath + @"\save.txt");
                string login = sr.ReadLine();
                string pass = sr.ReadLine();
                sr.Close();
                tb_login.Text = login;
                tb_password.Password = pass;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if(tb_login.Text.Length > 0)
            {
                if(tb_password.Password.Length > 0)
                {
                    MySqlDataReader user_count = mainWindow.Query(String.Format("SELECT count(*) " +
                        "FROM `users` " +
                        "INNER JOIN `user_phone` ON (`users`.id = `user_phone`.user_id) " +
                        "INNER JOIN `user_address` ON (`users`.id = `user_address`.id_user) " +
                        "WHERE `username` = '{0}' AND `password` = '{1}';", tb_login.Text, tb_password.Password));
                    user_count.Read();
                    int count = Convert.ToInt32(user_count.GetValue(0).ToString());
                    if (count == 0)
                    {
                        MessageBox.Show("Логин или пароль неверны", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    MySqlDataReader user = mainWindow.Query(String.Format("SELECT `users`.id, `users`.surname, `users`.name, `users`.patronymic, `user_phone`.phone, `users`.`e-mail`, `users`.role, " +
                        "`user_address`.street, `user_address`.house, `user_address`.apartment, `user_address`.entrance, `user_address`.floor, `users`.username, `users`.password, `users`.date " +
                        "FROM `users` " +
                        "INNER JOIN `user_phone` ON (`users`.id = `user_phone`.user_id) " +
                        "INNER JOIN `user_address` ON (`users`.id = `user_address`.id_user) " +
                        "WHERE `username` = '{0}' AND `password` = '{1}' ORDER BY `id` LIMIT {2},{3};", tb_login.Text, tb_password.Password, count-1,count));
                    if (user == null) return;
                    if (!user.HasRows)
                    {
                        MessageBox.Show("Логин или пароль неверны", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    if (user.Read())
                    {
                        string category = user.GetValue(0).ToString();

                        mainWindow.userInfo.id = Convert.ToInt32(user.GetValue(0).ToString());
                        mainWindow.userInfo.first_name = user.GetValue(1).ToString();
                        mainWindow.userInfo.name = user.GetValue(2).ToString();
                        mainWindow.userInfo.last_name = user.GetValue(3).ToString();
                        mainWindow.userInfo.phone = user.GetValue(4).ToString();
                        mainWindow.userInfo.mail = user.GetValue(5).ToString();
                        mainWindow.userInfo.street = user.GetValue(7).ToString();
                        mainWindow.userInfo.house = user.GetValue(8).ToString();
                        mainWindow.userInfo.room = user.GetValue(9).ToString();
                        mainWindow.userInfo.porch = user.GetValue(10).ToString();
                        mainWindow.userInfo.floor = user.GetValue(11).ToString();
                        mainWindow.userInfo.date = user.GetValue(14).ToString();
                        mainWindow.userInfo.login = user.GetValue(12).ToString();
                        mainWindow.userInfo.password = user.GetValue(13).ToString();
                        mainWindow.userInfo.role = Convert.ToInt32(user.GetValue(6).ToString());
                    }
                    if(save.IsChecked == true)
                    {
                        if (File.Exists(mainWindow.localPath + @"\save.txt"))
                        {
                            File.Delete((mainWindow.localPath + @"\save.txt"));
                        }
                        StreamWriter sw = new StreamWriter(mainWindow.localPath + @"\save.txt");
                        sw.WriteLine(tb_login.Text);
                        sw.WriteLine(tb_password.Password);
                        sw.Close();
                    }
                    user.Close();
                    if(mainWindow.userInfo.role == 0)
                    {
                        var ran = new Random();
                        int num = ran.Next(0, 100);
                        if (num > 90) new ToastContentBuilder()
                                 .AddArgument("conversationId", 9813)
                                 .AddText("#ХочуПиццу")
                                 .AddText("Поздравляю! Вы один из тех кто выиграли бесплатную пиццу. Приятного аппетита!")
                                 .Show();
                        mainWindow.ordersBook.Clear();
                        mainWindow.OpenPage(MainWindow.pages.main);
                    }
                    else if (mainWindow.userInfo.role == 1)
                    {
                        mainWindow.OpenPage(MainWindow.pages.admin);
                    }
                    else
                    {
                        MessageBox.Show("Такой пользователь не был найден.", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("Введите логин.", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        private void RegIn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.registration);
        }
    }
}
