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
    /// Логика взаимодействия для Mix_X.xaml
    /// </summary>
    public partial class Mix_X : Page
    {
        MainWindow mainWindow;
        List<Ingridient> ingr_list = new List<Ingridient>();
        int ind_check = 0;
        string size_string = "";
        public Mix_X(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            priceCount.Content = 200;
            wesCount.Content = 150;
            Render_Ingrid();
        }
        public void Render_Ingrid()
        {
            MySqlDataReader ingrids_data = mainWindow.Query("SELECT * FROM ingridient");
            if (ingrids_data == null) return;
            while (ingrids_data.Read())
            {
                Ingridient ingridient = new Ingridient();
                ingridient.id = Convert.ToInt32(ingrids_data.GetValue(0).ToString());
                ingridient.name = ingrids_data.GetValue(1).ToString();
                ingridient.weight = ingrids_data.GetValue(2).ToString();
                ingridient.price = ingrids_data.GetValue(3).ToString();

                ingr_list.Add(ingridient);
                parrent.Children.Add(new Elements.mix_itm(mainWindow, ingridient, 0));
            }
        }
        public void Update_data()
        {
            switch (ind_check)
            {
                case 0:
                    {
                        priceCount.Content = mainWindow.price_mix + 200;
                        wesCount.Content = mainWindow.weight_mix + 150;
                        break;
                    };
                case 1:
                    {
                        priceCount.Content = mainWindow.price_mix + 400;
                        wesCount.Content = mainWindow.weight_mix + 300;
                        break;
                    };
                case 2:
                    {
                        priceCount.Content = mainWindow.price_mix + 600;
                        wesCount.Content = mainWindow.weight_mix + 450;
                        break;
                    };
                default:
                    {
                        priceCount.Content = mainWindow.price_mix;
                        wesCount.Content = mainWindow.weight_mix;
                        break;
                    };
            }
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.main);
        }

        private void Search2(object sender, TextChangedEventArgs e)
        {
            List<Ingridient> ingrid_temp = ingr_list.FindAll(x => x.name.ToLower().Contains(serch_ing.Text.ToLower()));
            parrent.Children.Clear();
            foreach(Ingridient ingrid in ingrid_temp)
            {
                Ingridient search_ingrid = mainWindow.ingrid_list_mix.Find(x => x.id == ingrid.id);
                if(search_ingrid != null)
                {
                    parrent.Children.Add(new Elements.mix_itm(mainWindow, ingrid, search_ingrid.quantity));
                }
                else
                {
                    parrent.Children.Add(new Elements.mix_itm(mainWindow, ingrid, 0));
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (namePizz.Text.Length > 0)
            {
                if (mainWindow.ingrid_list_mix.Count > 0)
                {
                    mainWindow.Query(String.Format("INSERT INTO `dish` (name,pic,description,is_mix)" +
                        "VALUES ('{0}','{1}','{2}','{3}');", namePizz.Text, "", "", 1));

                    MySqlDataReader id_reader = mainWindow.Query(String.Format("SELECT `id` FROM `dish` " +
                        "WHERE `name` = '{0}' AND `pic` = '{1}' AND `description` = '{2}' AND `is_mix` = '{3}' ORDER BY `id` DESC LIMIT 1;", namePizz.Text, "", "", 1));
                    id_reader.Read();
                    int dish_id = Convert.ToInt32(id_reader.GetValue(0));
                    id_reader.Close();

                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                        "VALUES ('{0}','{1}','{2}','{3}')", dish_id, 23, mainWindow.price_mix + 200, mainWindow.weight_mix + 150));
                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                        "VALUES ('{0}','{1}','{2}','{3}')", dish_id, 30, mainWindow.price_mix + 400, mainWindow.weight_mix + 300));
                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                        "VALUES ('{0}','{1}','{2}','{3}')", dish_id, 40, mainWindow.price_mix + 600, mainWindow.weight_mix + 450));

                    string ingrids = "";
                    foreach(Ingridient ingrid in mainWindow.ingrid_list_mix)
                    {
                        ingrids += $"{ingrid.name},";
                    }
                    Update_data();
                    int size_id = 0;                  
                    MySqlDataReader size_id_reader = mainWindow.Query($"SELECT `id` FROM `size` WHERE `price` = '{priceCount.Content}' AND `weight` = '{wesCount.Content}' AND `id_dish` = '{dish_id}'");
                    size_id_reader.Read();
                    size_id = Convert.ToInt32(size_id_reader.GetValue(0).ToString());
                    size_id_reader.Close();
                    foreach (Ingridient ingrid in mainWindow.ingrid_list_mix)
                    {
                        mainWindow.Query(String.Format("INSERT INTO `dish_ingridient` (id_dish, id_ingridient) " +
                            "VALUES ('{0}', '{1}');", dish_id, ingrid.id));
                    }
                    new ToastContentBuilder()
                        .AddArgument("conversationId", 9813)
                        .AddText("#ХочуПиццу")
                        .AddText("Пицца успешно добавлена!")
                        .Show();                   
                    Order order = new Order();
                    order.id = dish_id;
                    order.list_id = mainWindow.ordersBook.Count();
                    order.id_size = size_id;
                    order.title = namePizz.Text.ToString();
                    order.price = Convert.ToInt32(priceCount.Content.ToString());
                    order.weight = Convert.ToInt32(wesCount.Content.ToString());
                    order.size_coul = size_string;
                    order.ingrid = ingrids;
                    order.count = 1;
                    order.src = "";
                    mainWindow.ordersBook.Add(order);
                    mainWindow.ingrid_list_mix.Clear();
                    mainWindow.price_mix = 0;
                    mainWindow.weight_mix = 0;
                    mainWindow.OpenPage(MainWindow.pages.main);
                }
                else
                {
                    MessageBox.Show("Добавьте хотя бы один игредиент", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Проверьте поле Названия", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
        }

        private void ChangeSize(object sender, RoutedEventArgs e)
        {
            RadioButton rat = (RadioButton)sender;
            ind_check = Convert.ToInt32(rat.Tag.ToString());
            size_string = rat.Content.ToString();
            Update_data();
        }
    }
}
