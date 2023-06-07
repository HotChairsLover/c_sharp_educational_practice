using Microsoft.Toolkit.Uwp.Notifications;
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
    /// Логика взаимодействия для Add_dish.xaml
    /// </summary>
    public partial class Add_dish : Page
    {
        MainWindow mainWindow;
        List<Ingridient> ingridients = new List<Ingridient>();
        public Add_dish(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.ingridients_select = new List<Ingridient>();
            Render_Ingrid();
        }
        public void Render_Ingrid()
        {
            parrent_ingrid.Children.Clear();
            MySqlDataReader ingrids_data = mainWindow.Query("SELECT * FROM ingridient");
            ingridients.Clear();
            if (ingrids_data == null) return;
            while (ingrids_data.Read())
            {
                Ingridient ingridient = new Ingridient();
                ingridient.id = Convert.ToInt32(ingrids_data.GetValue(0).ToString());
                ingridient.name = ingrids_data.GetValue(1).ToString();
                ingridient.weight = ingrids_data.GetValue(2).ToString();
                ingridient.price = ingrids_data.GetValue(3).ToString();

                ingridients.Add(ingridient);
            }
            foreach (Ingridient ingridient in ingridients)
            {
                parrent_ingrid.Children.Add(new Elements.Ingrid_select(mainWindow, ingridient));
            }
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
                            if(price1.Text.Length > 0 && price2.Text.Length > 0 && price3.Text.Length > 0 && price1.Text.All(char.IsDigit) && price2.Text.All(char.IsDigit) && price3.Text.All(char.IsDigit))
                            {
                                if(weight1.Text.Length > 0 && weight2.Text.Length > 0 && weight3.Text.Length > 0 && weight1.Text.All(char.IsDigit) && weight2.Text.All(char.IsDigit) && weight3.Text.All(char.IsDigit))
                                {
                                    mainWindow.Query(String.Format("INSERT INTO `dish` (name,pic,description,is_mix)" +
                                        "VALUES ('{0}','{1}','{2}','{3}');", name.Text, picter.Text, more.Text, 0));

                                    MySqlDataReader id_reader = mainWindow.Query(String.Format("SELECT `id` FROM `dish` " +
                                        "WHERE `name` = '{0}' AND `pic` = '{1}' AND `description` = '{2}' AND `is_mix` = '{3}';", name.Text, picter.Text, more.Text, 0));
                                    id_reader.Read();
                                    int dish_id = Convert.ToInt32(id_reader.GetValue(0));
                                    id_reader.Close();

                                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                                        "VALUES ('{0}','{1}','{2}','{3}')",dish_id, 23, price1.Text, weight1.Text));
                                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                                        "VALUES ('{0}','{1}','{2}','{3}')", dish_id, 30, price2.Text, weight2.Text));
                                    mainWindow.Query(String.Format("INSERT INTO `size` (id_dish,size,price,weight)" +
                                        "VALUES ('{0}','{1}','{2}','{3}')", dish_id, 40, price3.Text, weight3.Text));

                                    foreach(Ingridient ingrid in mainWindow.ingridients_select)
                                    {
                                        mainWindow.Query(String.Format("INSERT INTO `dish_ingridient` (id_dish, id_ingridient) " +
                                            "VALUES ('{0}', '{1}');", dish_id, ingrid.id));
                                    }
                                    new ToastContentBuilder()
                                        .AddArgument("conversationId", 9813)
                                        .AddText("#ХочуПиццу")
                                        .AddText("Пицца успешно добавлена!")
                                        .Show();
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
            if(openFileDialog.ShowDialog() == true)
            {
                string[] str = openFileDialog.FileName.Split('\\');
                picter.Text = str[str.Length-1];
                if (!File.Exists(mainWindow.localPath + @"\image/dish/" + picter.Text))
                {
                    File.Copy(openFileDialog.FileName, mainWindow.localPath + @"\image/dish/" + picter.Text);
                }
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            List<Ingridient> ingrid_temp = ingridients.FindAll(x => (x.name.ToLower().Contains(find.Text.ToLower())));
            parrent_ingrid.Children.Clear();
            foreach(Ingridient ingrid in ingrid_temp)
            {
                Ingridient search_ingrid = mainWindow.ingridients_select.Find(x => x.id == ingrid.id);
                if (search_ingrid != null)
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
