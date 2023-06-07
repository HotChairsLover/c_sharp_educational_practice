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
    /// Логика взаимодействия для Ingrid_change.xaml
    /// </summary>
    public partial class Ingrid_change : Page
    {
        MainWindow mainWindow;
        Ingridient igrid;
        public Ingrid_change(MainWindow mainWindow, Ingridient igrid)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.igrid = igrid;

            title.Text = igrid.name;
            weight.Text = igrid.weight;
            price.Text = igrid.price;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            mainWindow.Query($"UPDATE `ingridient` SET `name` = '{title.Text}', `weight` = '{weight.Text}', `price` = '{price.Text}' WHERE `id` = '{igrid.id}'");
            mainWindow.OpenPage(MainWindow.pages.admin);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.admin);
        }
    }
}
