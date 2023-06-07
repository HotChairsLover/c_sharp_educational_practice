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
    /// Логика взаимодействия для mix_itm.xaml
    /// </summary>
    public partial class mix_itm : UserControl
    {
        MainWindow mainWindow;
        Ingridient ingrid;
        public mix_itm(MainWindow mainWindow, Ingridient ingrid, int count)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.ingrid = ingrid;
            title.Content = ingrid.name;
            price.Content = "Цена: " + ingrid.price;
            weight.Content = "Вес: " + ingrid.weight;
            col_vo.Text = count.ToString();
        }

        private void move_col_Click(object sender, RoutedEventArgs e)
        {
            Button btn_move = (Button)sender;

            if (btn_move.Content.ToString() == "+")
            {
                if (Convert.ToInt32(col_vo.Text) < 15)
                {
                    col_vo.Text = (Convert.ToInt32(col_vo.Text) + 1).ToString();
                    mainWindow.price_mix += Convert.ToInt32(ingrid.price);
                    mainWindow.weight_mix += Convert.ToInt32(ingrid.weight);
                    ingrid.quantity++;
                    if (Convert.ToInt32(col_vo.Text) == 1)
                    {
                        mainWindow.ingrid_list_mix.Add(ingrid);
                    }
                    else
                    {
                        Ingridient ingr = mainWindow.ingrid_list_mix.Find(x => x.id == ingrid.id);
                        if(ingr != null)
                        {
                            ingr.quantity = Convert.ToInt32(col_vo.Text) + 1;
                        }
                    }
                }
            }
            else if (btn_move.Content.ToString() == "-")
            {
                if (Convert.ToInt32(col_vo.Text) > 0)
                {
                    col_vo.Text = (Convert.ToInt32(col_vo.Text) - 1).ToString();
                    mainWindow.price_mix -= Convert.ToInt32(ingrid.price);
                    mainWindow.weight_mix -= Convert.ToInt32(ingrid.weight);
                    if (Convert.ToInt32(col_vo.Text) == 0)
                    {
                        mainWindow.ingrid_list_mix.Remove(ingrid);
                    }
                    else
                    {
                        Ingridient ingr = mainWindow.ingrid_list_mix.Find(x => x.id == ingrid.id);
                        if (ingr != null)
                        {
                            ingr.quantity = Convert.ToInt32(col_vo.Text) - 1;
                        }
                    }
                }
            }
        }
    }
}
