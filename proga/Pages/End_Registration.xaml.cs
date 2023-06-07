using Microsoft.Toolkit.Uwp.Notifications;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для End_Registration.xaml
    /// </summary>
    public partial class End_Registration : Page
    {
        MainWindow mainWindow;
        public End_Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            CreatePassword();
            CreateUser();
        }
        public void CreatePassword()
        {
            string password = "";
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                password += (char)random.Next(0x0410, 0x44F);
            }
            mainWindow.userInfo.password = password;
            mainWindow.userInfo.role = 0;
        }
        public void CreateUser()
        {
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
                                 .AddText("Аккаунт был создан! Скопируйте пароль и логин!")
                                 .Show();
        }

        private void result_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string data = String.Format("{0}", mainWindow.userInfo.password);
            Thread thread = new Thread(() => Clipboard.SetText(data));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            //Clipboard.SetText(data);
            new ToastContentBuilder()
                                 .AddArgument("conversationId", 9813)
                                 .AddText("#ХочуПиццу")
                                 .AddText("Пароль скопирован в буфер обмена!")
                                 .Show();
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.main);
        }
    }
}
