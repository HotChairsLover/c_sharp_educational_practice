using Microsoft.Win32;
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

namespace proga.Pages
{
    /// <summary>
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Page
    {
        MainWindow mainWindow;
        public Check(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            MySqlDataReader data_orders = mainWindow.Query("SELECT count(id) FROM `orders`");

            if (data_orders == null) return;
            while (data_orders.Read())
            {
                TextBoxCheck.Text = String.Format("Кассовый чек\n#ХочуПиццу\nИНН 4634879675\n\nЧек №{0}\n{1}\nПермь\n{2} {3} {4}\nАдрес доставки: ул. {5} д. {6} кв. {7} ",
                    Convert.ToInt32(data_orders.GetValue(0))+1, DateTime.Now, mainWindow.userInfo.first_name, mainWindow.userInfo.name, mainWindow.userInfo.last_name,
                    mainWindow.userInfo.street, mainWindow.userInfo.house, mainWindow.userInfo.room);
                if(mainWindow.userInfo.porch != " ")
                {
                    TextBoxCheck.Text += $"под. {mainWindow.userInfo.porch} ";
                }
                if (mainWindow.userInfo.floor != " ")
                {
                    TextBoxCheck.Text += $"эт. {mainWindow.userInfo.floor} ";
                }
                TextBoxCheck.Text += "\n\nТОВАР\n";
                int sum = 0;
                foreach(Order order in mainWindow.ordersBook)
                {
                    TextBoxCheck.Text += $"({order.count}x){order.title}({order.size_coul}){order.price}\n";
                    sum += order.price * order.count;
                }
                TextBoxCheck.Text += $"\n\nИТОГ {sum} руб.";
                int discount = 0;
                if(DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    discount = sum / 100 * 15;
                }
                TextBoxCheck.Text += $"\nСКИДКА {discount} руб.";
                TextBoxCheck.Text += $"\nК ОПЛАТЕ {sum-discount} руб.";
                TextBoxCheck.Text += $"\n\nОПЛАЧЕНО {sum-discount} руб.";
                TextBoxCheck.Text += $"\nСУММА К НДС 20% {20 * (sum-discount) / 100} руб.";
                TextBoxCheck.Text += $"\nСАЙТ НДС www.nalog.ru";
            }
            data_orders.Close();
            mainWindow.ordersBook.Clear();

        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.main);
        }

        private void SaveCheck(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if(saveFileDialog.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
                sw.Write(TextBoxCheck.Text);
                sw.Close();
            }
        }
    }
}
