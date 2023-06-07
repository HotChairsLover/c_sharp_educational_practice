using Microsoft.Win32;
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

namespace proga.Pages.Change
{
    /// <summary>
    /// Логика взаимодействия для Dish_change.xaml
    /// </summary>
    public partial class Dish_change : Page
    {
        MainWindow mainWindow;
        Dish dish_inf;
        List<Ingridient> list_ingrid = new List<Ingridient>();
        public Dish_change(MainWindow mainWindow, Dish dish_inf = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.dish_inf = dish_inf;

            name.Text = dish_inf.name;
            more.Text = dish_inf.description;
            picter.Text = dish_inf.src;

            mainWindow.ingridients_select = new List<Ingridient>(dish_inf.dish_Ingrids);
            MySqlDataReader ingrids_data = mainWindow.Query("SELECT * FROM ingridient");
            list_ingrid.Clear();
            if (ingrids_data == null) return;
            while (ingrids_data.Read())
            {
                Ingridient ingridient = new Ingridient();
                ingridient.id = Convert.ToInt32(ingrids_data.GetValue(0).ToString());
                ingridient.name = ingrids_data.GetValue(1).ToString();
                ingridient.weight = ingrids_data.GetValue(2).ToString();
                ingridient.price = ingrids_data.GetValue(3).ToString();

                list_ingrid.Add(ingridient);
            }


            foreach (Ingridient ingrid in list_ingrid)
            {
                Ingridient search_ingrid = dish_inf.dish_Ingrids.Find(x => x.id == ingrid.id);
                if(search_ingrid != null)
                {
                    parrent_ingrid.Children.Add(new Elements.Ingrid_select(mainWindow, ingrid, true));
                }
                else
                {
                    parrent_ingrid.Children.Add(new Elements.Ingrid_select(mainWindow, ingrid, false));
                }
            }
            price1.Text = dish_inf.sizes_dish[0].price.ToString();
            price2.Text = dish_inf.sizes_dish[1].price.ToString();
            price3.Text = dish_inf.sizes_dish[2].price.ToString();

            weight1.Text = dish_inf.sizes_dish[0].weight.ToString();
            weight2.Text = dish_inf.sizes_dish[1].weight.ToString();
            weight3.Text = dish_inf.sizes_dish[2].weight.ToString();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if(name.Text.Length > 0)
            {
                if(more.Text.Length > 0)
                {
                    if(picter.Text.Length > 0)
                    {
                        if(mainWindow.ingridients_select.Count >= 1)
                        {
                            if(price1.Text.Length > 0 && price2.Text.Length > 0 && price3.Text.Length > 0)
                            {
                                if (weight1.Text.Length > 0 && weight2.Text.Length > 0 && weight3.Text.Length > 0)
                                {
                                    mainWindow.Query($"UPDATE `size` SET `price` = '{price1.Text}', `weight` = '{weight1.Text}' WHERE `id_dish` = '{dish_inf.id}' AND `size`.`size` = '23'");
                                    mainWindow.Query($"UPDATE `size` SET `price` = '{price2.Text}', `weight` = '{weight2.Text}' WHERE `id_dish` = '{dish_inf.id}' AND `size`.`size` = '30'");
                                    mainWindow.Query($"UPDATE `size` SET `price` = '{price3.Text}', `weight` = '{weight3.Text}' WHERE `id_dish` = '{dish_inf.id}' AND `size`.`size` = '40'");

                                    foreach(Ingridient dish_ingrid in mainWindow.ingridients_select)
                                    {
                                        Ingridient search_ingrid = dish_inf.dish_Ingrids.Find(x => x.id == dish_ingrid.id);
                                        if(search_ingrid == null)
                                        {
                                            mainWindow.Query($"INSERT INTO `dish_ingridient` (`id_dish`,`id_ingridient`) VALUES ('{dish_inf.id}','{dish_ingrid.id}');");
                                        }
                                    }
                                    foreach(Ingridient dish_ingrid in dish_inf.dish_Ingrids)
                                    {
                                        Ingridient search_ingrid = mainWindow.ingridients_select.Find(x => x.id == dish_ingrid.id);
                                        if(search_ingrid == null)
                                        {
                                            mainWindow.Query($"DELETE FROM `dish_ingridient` WHERE `id_dish` = '{dish_inf.id}' AND `id_ingridient` = '{dish_ingrid.id}';");
                                        }
                                    }
                                    mainWindow.Query($"UPDATE `dish` SET `name` = '{name.Text}', `pic` = '{picter.Text}',`description` = '{more.Text}' WHERE `id` = '{dish_inf.id}'");
                                    mainWindow.OpenPage(MainWindow.pages.admin);
                                }
                                else
                                {
                                    MessageBox.Show("Проверьте поля веса", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Проверьте поля цен", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Добавьте хотя бы один игредиент", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте поле Изображения", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте поле Описания", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Проверьте поле Названия", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.admin);
        }

        private void TreePicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] str = openFileDialog.FileName.Split('\\');
                picter.Text = str[str.Length - 1];
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            List<Ingridient> ingrid_temp = list_ingrid.FindAll(x => x.name.ToLower().Contains(find.Text.ToLower()));
            parrent_ingrid.Children.Clear();
            foreach(Ingridient ingrid in ingrid_temp)
            {
                Ingridient search_ingrid = dish_inf.dish_Ingrids.Find(x => x.id == ingrid.id);
                if(search_ingrid != null)
                {
                    parrent_ingrid.Children.Add(new Elements.Ingrid_select(mainWindow, ingrid, true));
                }
                else
                {
                    parrent_ingrid.Children.Add(new Elements.Ingrid_select(mainWindow, ingrid, false));
                }
            }
        }
    }
}
