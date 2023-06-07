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
    /// Логика взаимодействия для Ingrid_select.xaml
    /// </summary>
    public partial class Ingrid_select : UserControl
    {
        MainWindow mainWindow;
        Ingridient ingrid;
        public Ingrid_select(MainWindow mainWindow, Ingridient ingrid, bool select = false)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.ingrid = ingrid;

            title.Content = ingrid.name;
            price.Content = "Цена: " + ingrid.price;
            weigth.Content = "Вес: " + ingrid.weight;
            check.IsChecked = select;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(check.IsChecked.Value.ToString() == "True")
            {
                mainWindow.ingridients_select.Add(ingrid);
            }
            else
            {
                foreach(Ingridient ingridient in mainWindow.ingridients_select)
                {
                    if(ingridient.id == ingrid.id)
                    {
                        mainWindow.ingridients_select.Remove(ingridient);
                        break;
                    }
                }
               // int index_of_delete = mainWindow.ingridients_select.IndexOf(ingrid);
                
            }
        }
    }
}
