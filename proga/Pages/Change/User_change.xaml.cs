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

namespace proga.Pages.Change
{
    /// <summary>
    /// Логика взаимодействия для User_change.xaml
    /// </summary>
    public partial class User_change : Page
    {
        MainWindow mainWindow;
        User user;
        public User_change(MainWindow mainWindow, User user = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.user = user;

            Index.Text = user.id.ToString();
            Email.Text = user.mail;
            FirstName.Text = user.first_name;
            name.Text = user.name;
            LastName.Text = user.last_name;
            Phone.Text = user.phone;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            mainWindow.Query($"UPDATE `users` SET `surname` = '{FirstName.Text}', `name` = '{name.Text}', `patronymic` = '{LastName.Text}', `e-mail` = '{Email.Text}', `username` = '{Email.Text}' WHERE `id` = '{Index.Text}'");
            mainWindow.Query($"UPDATE `user_phone` SET `phone` = '{Phone.Text}' WHERE `user_id` = '{Index.Text}' AND `phone` = '{user.phone}';");
            mainWindow.OpenPage(MainWindow.pages.admin);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.admin);
        }
    }
}
