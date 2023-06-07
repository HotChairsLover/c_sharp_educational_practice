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
    /// Логика взаимодействия для Redac_orders.xaml
    /// </summary>
    public partial class Redac_orders : Page
    {
        MainWindow mainWindow;
        public Redac_orders(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            
            RenderPizz();
        }
        public void RenderPizz()
        {
            foreach (Order order in mainWindow.ordersBook)
            {
                parrent_orders.Children.Add(new Elements.Redac_tab(mainWindow, order));
            }
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.main);
        }

        private void OrdersForm(object sender, RoutedEventArgs e)
        {
            if(parrent_orders.Children.Count > 0)
            {
                mainWindow.OpenPage(MainWindow.pages.orders);
            }
        }
    }
}
