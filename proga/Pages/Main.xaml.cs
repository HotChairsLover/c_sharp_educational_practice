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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow;
        List<Dish> dishes = new List<Dish>();
        public Main(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            RenderPizza();
        }
        public void RenderPizza()
        {
            MySqlDataReader dishs_data = mainWindow.Query("SELECT * FROM `dish`" +
                "INNER JOIN `dish_ingridient` ON (`dish`.id = `dish_ingridient`.id_dish)" +
                "INNER JOIN `ingridient` ON (`dish_ingridient`.id_ingridient = `ingridient`.id)" +
                "INNER JOIN `size` ON (`size`.id_dish = `dish`.id) WHERE `is_mix` = 0");
            int n = 0;
            if (dishs_data == null) return;
            while (dishs_data.Read())
            {
                int y = 0;
                if (n == 0 || !dishes.Exists(itm => itm.id == Convert.ToInt32(dishs_data.GetValue(0))))
                {
                    Dish dish = new Dish();
                    dish.id = Convert.ToInt32(dishs_data.GetValue(0).ToString());
                    dish.name = dishs_data.GetValue(1).ToString();
                    dish.src = dishs_data.GetValue(2).ToString();
                    dish.description = dishs_data.GetValue(3).ToString();

                    dish.dish_Ingrids.Add(new Ingridient()
                    {
                        id = Convert.ToInt32(dishs_data.GetValue(8).ToString()),
                        name = dishs_data.GetValue(9).ToString(),
                        weight = dishs_data.GetValue(10).ToString(),
                        price = dishs_data.GetValue(11).ToString()
                    });
                    dish.sizes_dish.Add(new Size()
                    {
                        id = Convert.ToInt32(dishs_data.GetValue(12).ToString()),
                        id_size = Convert.ToInt32(dishs_data.GetValue(13).ToString()),
                        size = Convert.ToInt32(dishs_data.GetValue(14).ToString()),
                        price = Convert.ToInt32(dishs_data.GetValue(15).ToString()),
                        weight = Convert.ToInt32(dishs_data.GetValue(16).ToString())
                    });

                    dishes.Add(dish);
                }
                else
                {
                    Dish dish = dishes.Find(x => x.id == Convert.ToInt32(dishs_data.GetValue(0).ToString()));
                    if (dish != null)
                    {
                        if (!dish.dish_Ingrids.Exists(itm => itm.name == dishs_data.GetValue(9).ToString()))
                        {
                            dish.dish_Ingrids.Add(new Ingridient()
                            {
                                id = Convert.ToInt32(dishs_data.GetValue(8).ToString()),
                                name = dishs_data.GetValue(9).ToString(),
                                weight = dishs_data.GetValue(10).ToString(),
                                price = dishs_data.GetValue(11).ToString()
                            });
                        }
                        if (!dish.sizes_dish.Exists(itm => itm.size == Convert.ToInt32(dishs_data.GetValue(14).ToString())))
                        {
                            dish.sizes_dish.Add(new Size()
                            {
                                id = Convert.ToInt32(dishs_data.GetValue(12).ToString()),
                                id_size = Convert.ToInt32(dishs_data.GetValue(13).ToString()),
                                size = Convert.ToInt32(dishs_data.GetValue(14).ToString()),
                                price = Convert.ToInt32(dishs_data.GetValue(15).ToString()),
                                weight = Convert.ToInt32(dishs_data.GetValue(16).ToString())
                            });
                        }
                    }

                }
                n++;
                y++;
            }
            dishs_data.Close();
            foreach(Dish dish in dishes)
            {
                parrent.Children.Add(new Elements.Main_tab(mainWindow, dish));
            }
        }

        private void OpenMix(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.mix_x);
        }

        private void OpenOrders(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.redac_orders);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.authorization);
        }
    }
}
