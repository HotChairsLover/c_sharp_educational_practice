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
    /// Логика взаимодействия для Add_ingridient.xaml
    /// </summary>
    public partial class Add_ingridient : Page
    {
        MainWindow mainWindow;
        public Add_ingridient(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (title.Text.Length > 0 && title.Text.Length <= 40)
            {
                if (weight.Text.Length > 0 && weight.Text.Length <= 5 && weight.Text.All(char.IsDigit))
                {
                    if (price.Text.Length > 0 && price.Text.Length <= 5 && price.Text.All(char.IsDigit))
                    {
                        MySqlDataReader ingrid = mainWindow.Query(String.Format("SELECT * FROM `ingridient` WHERE name = '{0}';", title.Text));
                        if (ingrid == null) return;
                        if (!ingrid.Read())
                        {
                            mainWindow.Query(String.Format("INSERT INTO `ingridient` (name,weight,price)" +
                                "VALUES ('{0}','{1}','{2}');", title.Text, weight.Text, price.Text));
                            new ToastContentBuilder()
                                        .AddArgument("conversationId", 9813)
                                        .AddText("#ХочуПиццу")
                                        .AddText("Игредиент успешно добавлен!")
                                        .Show();
                            mainWindow.OpenPage(MainWindow.pages.admin);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте поле Цена", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте поле Вес", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Проверьте поле Имя", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.admin);
        }
    }
}
