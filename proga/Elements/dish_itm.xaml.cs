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

namespace proga.Elements
{
    /// <summary>
    /// Логика взаимодействия для dish_itm.xaml
    /// </summary>
    public partial class dish_itm : UserControl
    {
        MainWindow mainWindow;
        Dish item;
        public dish_itm(MainWindow mainWindow, Dish item)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.item = item;
            title.Content = item.name;
            text.Text = item.description;
            price.Content = "Цена: " + item.sizes_dish[0].price;
            weight.Content = "Вес: " + item.sizes_dish[0].weight;
            string ingidsStr = "";
            foreach(Ingridient ingrid in item.dish_Ingrids)
            {
                ingidsStr += ingrid.name + ", ";
            }
            ingridients.Text = ingidsStr;
            BitmapImage pizzaImg = new BitmapImage();
            pizzaImg.BeginInit();
            if(File.Exists(mainWindow.localPath + @"\image/dish/" + item.src))
            {
                pizzaImg.UriSource = new Uri(mainWindow.localPath + @"\image/dish/" + item.src);
            }
            else
            {
                pizzaImg.UriSource = new Uri(mainWindow.localPath + @"\image/logo.png");
            }
            pizzaImg.EndInit();
            pic.Source = pizzaImg;
        }

        private void Del_dish(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы хотите удалить пиццу?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //mainWindow.Query(String.Format("DELETE FROM `dish_ingridient` WHERE `id_dish` = {0};",item.id));
                //mainWindow.Query(String.Format("DELETE FROM `size` WHERE `id_dish` = {0};", item.id));
                mainWindow.Query(String.Format("DELETE FROM `dish` WHERE `id` = {0};", item.id));
                mainWindow.OpenPage(MainWindow.pages.admin);
            }
        }

        private void Change_dish(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.dish_chage, null, null, item);
        }
    }
}
