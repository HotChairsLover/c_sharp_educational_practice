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

namespace proga
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connection = "server=localhost;port=3307;database=mydb;uid=root";
        public User userInfo = new User();
        public List<Order> ordersBook = new List<Order>();
        public string localPath = System.IO.Directory.GetCurrentDirectory();

        public Pages.Mix_X mix;
        public int price_mix = 0;
        public int weight_mix = 0;
        public List<Ingridient> ingrid_list_mix = new List<Ingridient>();

        public Pages.Admin adm_page;

        public List<Ingridient> ingridients_select = new List<Ingridient>();

        public int add_dish_id = 0;
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.authorization);
        }
        public bool CheckMail(string text)
        {
            bool check = false;
            string[] info1 = text.Split('@');
            if (info1.Length == 2)
            {
                string[] info2 = info1[1].Split('.');
                if (info2.Length == 2)
                {
                    check = true;
                }
            }
            return check;
        }
        public bool CheckPhone(string phone)
        {
            if(phone[0] == '+' && phone.Length == 12)
            {
                string phone2 = phone.Substring(2);
                if(phone2.Length == 10)
                {
                    foreach (char c in phone2)
                    {
                        if (c < '0' || c > '9')
                            return false;
                    }
                    return true;
                }
                return false;
            }
            else if(phone.Length == 11)
            {
                foreach (char c in phone)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
                return true;
            }
            return false;
        }
        public MySqlDataReader Query(string query)
        {
            connection = "server=localhost;port=3307;database=mydb;uid=root";
            //connection = "server=localhost;port=3306;database=mydb;uid=root;pwd=root";
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader ticket_query = mySqlCommand.ExecuteReader();
                return ticket_query;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Программа не может быть запущена т.к сервер не отвечает", "Error connection server", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("Программа выявила какую-то ошибку", "Error connection server", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }
        public IConnectionWork IQuery(string query)
        {
            connection = "server=localhost;port=3307;database=mydb;uid=root";
            //connection = "server=localhost;port=3306;database=mydb;uid=root;pwd=root";
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader ticket_query = mySqlCommand.ExecuteReader();
                IConnectionWork data = new IConnectionWork();
                data.mySqlConnection = mySqlConnection;
                data.mySqlDataReader = ticket_query;
                return data;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Программа не может быть запущена т.к сервер не отвечает", "Error connection server", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("Программа выявила какую-то ошибку", "Error connection server", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }
        public void Update_MixPrice()
        {
            //mix.Update_data();
        }
        public void Update_AdminUser()
        {
            adm_page.Render_Users();
        }
        public void Update_AdminDish()
        {
            adm_page.Render_Dish();
        }
        public void Update_AdminIngrid()
        {
            adm_page.Render_Ingrid();
        }
        public enum pages
        {
            add_dish,add_ingridient,add_user,admin,authorization,check,end_registration,main,mix_x,orders,registration,redac_orders,user_change,ingrid_change,dish_chage
        }
        public void OpenPage(pages _pages, User user = null, Ingridient ingrid = null, Dish dish_inf = null)
        {
            if (_pages == pages.add_dish) frame.Navigate(new Pages.Add_dish(this));
            else if (_pages == pages.add_ingridient) frame.Navigate(new Pages.Add_ingridient(this));
            else if (_pages == pages.add_user) frame.Navigate(new Pages.Add_user(this));
            else if (_pages == pages.admin) frame.Navigate(new Pages.Admin(this));
            else if (_pages == pages.authorization) frame.Navigate(new Pages.Authorization(this));
            else if (_pages == pages.check) frame.Navigate(new Pages.Check(this));
            else if (_pages == pages.end_registration) frame.Navigate(new Pages.End_Registration(this));
            else if (_pages == pages.main) frame.Navigate(new Pages.Main(this));
            else if (_pages == pages.mix_x) frame.Navigate(new Pages.Mix_X(this));
            else if (_pages == pages.orders) frame.Navigate(new Pages.Orders(this));
            else if (_pages == pages.registration) frame.Navigate(new Pages.Registration(this));
            else if (_pages == pages.redac_orders) frame.Navigate(new Pages.Redac_orders(this));
            else if (_pages == pages.user_change) frame.Navigate(new Pages.Change.User_change(this, user));
            else if (_pages == pages.ingrid_change) frame.Navigate(new Pages.Change.Ingrid_change(this, ingrid));
            else if (_pages == pages.dish_chage) frame.Navigate(new Pages.Change.Dish_change(this, dish_inf));
        }
    }
}
