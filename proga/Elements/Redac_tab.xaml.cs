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

namespace proga.Elements
{
    /// <summary>
    /// Логика взаимодействия для Redac_tab.xaml
    /// </summary>
    public partial class Redac_tab : UserControl
    {
        MainWindow mainWindow;
        Order itm_order;
        public Redac_tab(MainWindow mainWindow, Order itm_order)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.itm_order = itm_order;

            title.Content = itm_order.title;
            ingridients.Text = itm_order.ingrid;
            price.Content = "Цена: " + itm_order.price;
            weight.Content = "Вес: " + itm_order.weight;
            size_coul.Content = "Размер: " + itm_order.size_coul;

            col_vo.Text = itm_order.count.ToString();

            if (itm_order.src != "")
            {
                BitmapImage pizzaImg = new BitmapImage();
                pizzaImg.BeginInit();
                pizzaImg.UriSource = new Uri(mainWindow.localPath + @"\image/dish/" + itm_order.src);
                pizzaImg.EndInit();
                pic.Source = pizzaImg;
            }
        }

        private void Del_dish(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить пиццу?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                mainWindow.ordersBook.Remove(itm_order);
                mainWindow.OpenPage(MainWindow.pages.redac_orders);
            }
        }

        private void move_col_Click(object sender, RoutedEventArgs e)
        {
            Button btn_move = (Button)sender;

            if(btn_move.Content.ToString() == "+" && Convert.ToInt32(col_vo.Text) < 15)
            {
                col_vo.Text = (Convert.ToInt32(col_vo.Text) + 1).ToString();
                mainWindow.ordersBook[itm_order.list_id].count = Convert.ToInt32(col_vo.Text);
            }
            else if (btn_move.Content.ToString() == "-" && Convert.ToInt32(col_vo.Text) > 1)
            {
                col_vo.Text = (Convert.ToInt32(col_vo.Text) - 1).ToString();
                mainWindow.ordersBook[itm_order.list_id].count = Convert.ToInt32(col_vo.Text);
            }
        }
    }
}
