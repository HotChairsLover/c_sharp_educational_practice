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
    /// Логика взаимодействия для Main_tab.xaml
    /// </summary>
    public partial class Main_tab : UserControl
    {
        MainWindow mainWindow;
        Dish item;

        public int select_id = 0;
        public string size_select = "23 см.";
        public Main_tab(MainWindow mainWindow, Dish item)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.item = item;

            title.Content = item.name;
            text.Text = item.description;

            price.Content = "Цена: " + item.sizes_dish[0].price;
            weight.Content = "Вес: " + item.sizes_dish[0].weight;

            string ingidsStr = "";
            foreach (Ingridient ingrid in item.dish_Ingrids)
            {
                ingidsStr += ingrid.name + ", ";
            }
            ingridients.Text = ingidsStr;
            BitmapImage pizzaImg = new BitmapImage();
            pizzaImg.BeginInit();
            pizzaImg.UriSource = new Uri(mainWindow.localPath + @"\image/dish/" + item.src);
            pizzaImg.EndInit();
            pic.Source = pizzaImg;
        }

        private void move_col_Click(object sender, RoutedEventArgs e)
        {
            Button btn_move = (Button)sender;

            if(btn_move.Content.ToString() == "+")
            {
                col_vo.Text = (Convert.ToInt32(col_vo.Text) + 1).ToString();
            }
            else if(btn_move.Content.ToString() == "-")
            {
                if(Convert.ToInt32(col_vo.Text) > 0)
                {
                    col_vo.Text = (Convert.ToInt32(col_vo.Text) - 1).ToString();
                }
            }
        }

        private void Ordering(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(col_vo.Text) > 0)
            {
                Order order = new Order();
                order.id = item.id;
                order.list_id = mainWindow.ordersBook.Count();
                order.id_size = item.sizes_dish[select_id].id;
                order.title = title.Content.ToString();
                order.price = item.sizes_dish[select_id].price;
                order.weight = item.sizes_dish[select_id].weight;
                order.size_coul = size_select;
                order.ingrid = ingridients.Text.ToString();
                order.count = Convert.ToInt32(col_vo.Text);
                order.src = item.src;

                if(mainWindow.ordersBook.Count < 1)
                {
                    mainWindow.ordersBook.Add(order);
                }
                else
                {
                    foreach(Order _order in mainWindow.ordersBook)
                    {
                        Order order_find = mainWindow.ordersBook.Find(x => x.id_size == _order.id_size);
                        if(order_find != null && order.id == order_find.id)
                        {
                            if (order_find.size_coul == order.size_coul)
                            {
                                order_find.count += Convert.ToInt32(col_vo.Text);
                            }
                            else
                            {
                                mainWindow.ordersBook.Add(order);
                                break;
                            }
                        }
                        else
                        {
                            mainWindow.ordersBook.Add(order);
                            break;
                        }
                    }
                }
                col_vo.Text = "0";
            }
            else
            {
                MessageBox.Show("Увеличьте количество пицц", "Error count zero", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PressSize(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            foreach(Button itm in group_btn.Children)
            {
                if(itm == btn)
                {
                    btn.Background = new SolidColorBrush(Color.FromArgb(255, 255, 201, 112));
                    btn.Foreground = Brushes.Black;
                    select_id = Convert.ToInt32(btn.Tag.ToString());
                    size_select = btn.Content.ToString();
;                }
                else
                {
                    btn.Background = new SolidColorBrush(Color.FromArgb(0, 255, 201, 112));
                    btn.Foreground = Brushes.White;
                }
            }
            price.Content = "Цена: " + item.sizes_dish.Find(it => it.size.ToString() == btn.Content.ToString().Replace(" см.", string.Empty)).price;
            weight.Content = "Вес: " + item.sizes_dish.Find(it => it.size.ToString() == btn.Content.ToString().Replace(" см.", string.Empty)).weight;
        }
    }
}
