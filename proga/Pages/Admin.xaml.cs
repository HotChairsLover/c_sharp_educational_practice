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
using Microsoft.Toolkit.Uwp.Notifications;

namespace proga.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        MainWindow mainWindow;
        List<User> users_arr = new List<User>();
        List<Dish> dishes = new List<Dish>();
        public List<Ingridient> ingridients = new List<Ingridient>();
        List<Order> orders = new List<Order>();
        List<Order> orders_statistics = new List<Order>();
        public Admin(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            users_arr.Clear();
            ingridients.Clear();

            Render_Users();
            Render_Dish();
            Render_Ingrid();
            Render_Orders();
            Render_Statistics();
        }
        public void Render_Statistics()
        {
            MySqlDataReader statis_data = mainWindow.Query("SELECT * FROM `orders`");

            if (statis_data == null) return;
            while (statis_data.Read())
            {
                Order user = new Order();
                user.id = Convert.ToInt32(statis_data.GetValue(0).ToString());
                user.date = statis_data.GetValue(3).ToString();
                user.count = Convert.ToInt32(statis_data.GetValue(7).ToString());

                orders_statistics.Add(user);
            }

            TimeSpan countDate = Convert.ToDateTime(DateTime.Now).Subtract(Convert.ToDateTime(orders_statistics[0].date));

            string[] dates = new string[orders_statistics.Count];
            for (int i = 0; i < orders_statistics.Count; i++)
            {
                dates[i] = orders_statistics[i].date;
            }
            dates = dates.Distinct().ToArray();

            int[] price = new int[dates.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                for (int j = 0; j < orders_statistics.Count; j++)
                {
                    if (dates[i] == orders_statistics[j].date)
                    {
                        price[i] += orders_statistics[j].count;
                    }
                }
            }
            int max = price.Max();
            int min = price.Min();

            canvas.Width = 100 * (Convert.ToInt32(countDate.Days) + 1) + 50;
            canvas.Height = max / 100 + 50;

            int step = 20;
            string[] countPoint = new string[Convert.ToInt32(countDate.Days) + 1];
            int countD = Convert.ToInt32(countDate.Days);
            string[] newInfo = new string[Convert.ToInt32(countDate.Days) + 1];

            for (int i = 0; i < countPoint.Length; i++)
            {
                for (int j = 0; j < dates.Length; j++)
                {
                    if (dates[j].Replace(" 0:00:00", null) == DateTime.Today.AddDays(-countD).ToString().Replace(" 0:00:00", null))
                    {
                        newInfo[i] = dates[j] + "|" + price[j];
                    }
                }
                countD--;
            }
            countD = Convert.ToInt32(countDate.Days);
            string info = string.Empty;

            for (int i = 0; i < newInfo.Length; i++)
            {
                if(newInfo[i] != null)
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Width = 8;
                    elipse.Height = 8;
                    elipse.StrokeThickness = 6;
                    elipse.Stroke = Brushes.Red;
                    elipse.Tag = i;
                    string date = DateTime.Today.AddDays(-countD).ToString().Replace(" 0:00:00", null);
                    string[] str = newInfo[i].Split('|');
                    elipse.MouseDown += delegate
                    {
                        MessageBox.Show(Convert.ToInt32(str[1]) + "зак.\n" + date);
                    };
                    int summ = Convert.ToInt32(canvas.Height) - Convert.ToInt32(str[1]);
                    elipse.Margin = new Thickness(step - 4, summ - 4, 0, 0);
                    countPoint[i] = step + "|" + summ;
                    step += 100;
                    canvas.Children.Add(elipse);
                }
                else
                {
                    Ellipse elipse = new Ellipse();
                    elipse.Width = 8;
                    elipse.Height = 8;
                    elipse.StrokeThickness = 6;
                    elipse.Stroke = Brushes.Red;
                    elipse.Tag = i;
                    string date = DateTime.Today.AddDays(-countD).ToString().Replace(" 0:00:00", null);
                    elipse.MouseDown += delegate
                    {
                        MessageBox.Show("0 зак.\n" + date);
                    };
                    int summ = Convert.ToInt32(canvas.Height) - (0 / 100);
                    elipse.Margin = new Thickness(step - 4, summ - 4, 0, 0);
                    countPoint[i] = step + "|" + summ;
                    step += 100;
                    canvas.Children.Add(elipse);
                }
                countD--;

                if(i != 0)
                {
                    string[] str1 = info.Split('|');
                    string[] str2 = countPoint[i].Split('|');

                    Line line = new Line();
                    line.X1 = Convert.ToDouble(str1[0]);
                    line.Y1 = Convert.ToDouble(str1[1]);
                    line.X2 = Convert.ToDouble(str2[0]);
                    line.Y2 = Convert.ToDouble(str2[1]);
                    line.Stroke = Brushes.Red;
                    canvas.Children.Add(line);
                }
                info = countPoint[i];
            }
        }
        public void Render_Users()
        {
            list_users.Items.Clear();
            /*MySqlDataReader users_data = mainWindow.Query("SELECT `users`.id, `users`.surname, `users`.name, `users`.patronymic, `user_phone`.phone, `users`.`e-mail`, `users`.role, " +
                "`user_address`.street, `user_address`.house, `user_address`.apartment, `user_address`.entrance, `user_address`.floor, `users`.username, `users`.password, `users`.date " +
                "FROM `users` " +
                "INNER JOIN `user_phone` ON (`users`.id = `user_phone`.user_id) " +
                "INNER JOIN `user_address` ON (`users`.id = `user_address`.id_user) " +
                "GROUP BY `users`.id, `user_phone`.phone, `user_address`.street, `user_address`.house, `user_address`.apartment, `user_address`.entrance, `user_address`.floor;");*/
            MySqlDataReader users_data = mainWindow.Query("SELECT `users`.id, `users`.surname, `users`.name, `users`.patronymic, `user_phone`.phone, `users`.`e-mail`, `users`.role, " +
                "`users`.username, `users`.password, `users`.date " +
                "FROM `users` " +
                "INNER JOIN `user_phone` ON (`users`.id = `user_phone`.user_id) " +
                "INNER JOIN `user_address` ON (`users`.id = `user_address`.id_user) " +
                "GROUP BY `users`.id, `user_phone`.phone;");
            users_arr.Clear();
            if (users_data == null) return;
            while (users_data.Read())
            {
                User user = new User();
                user.id = Convert.ToInt32(users_data.GetValue(0).ToString());
                user.first_name = users_data.GetValue(1).ToString();
                user.name = users_data.GetValue(2).ToString();
                user.last_name = users_data.GetValue(3).ToString();
                user.phone = users_data.GetValue(4).ToString();
                user.mail = users_data.GetValue(5).ToString();
                /*user.street = users_data.GetValue(7).ToString();
                user.house = users_data.GetValue(8).ToString();
                user.room = users_data.GetValue(9).ToString();
                user.porch = users_data.GetValue(10).ToString();
                user.floor = users_data.GetValue(11).ToString();
                user.date = users_data.GetValue(14).ToString();
                user.login = users_data.GetValue(12).ToString();
                user.password = users_data.GetValue(13).ToString();
                user.role = Convert.ToInt32(users_data.GetValue(6).ToString());*/
                user.date = users_data.GetValue(9).ToString();
                user.login = users_data.GetValue(7).ToString();
                user.password = users_data.GetValue(8).ToString();
                user.role = Convert.ToInt32(users_data.GetValue(6).ToString());
                users_arr.Add(user);
            }
            foreach (User user in users_arr)
            {
                list_users.Items.Add(user);
            }
        }
        public void Render_Dish()
        {
            parrent_dish.Children.Clear();
            MySqlDataReader dishs_data = mainWindow.Query("SELECT * FROM `dish` as dishs " +
                "INNER JOIN `dish_ingridient` as dish_ingrids ON (dishs.id = dish_ingrids.id_dish) " +
                "INNER JOIN `ingridient` AS ingrids ON (dish_ingrids.id_ingridient = ingrids.id) " +
                "INNER JOIN `size` AS sizes ON (sizes.id_dish = dishs.id) WHERE `is_mix` = 0;");
            int n = 0;
            if (dishs_data == null) return;
            dishes.Clear();
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

            foreach (Dish dish in dishes)
            {
                parrent_dish.Children.Add(new Elements.dish_itm(mainWindow, dish));
            }
        }
        public void Render_Ingrid()
        {
            list_ingredients.Items.Clear();
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
            foreach(Ingridient ingridient in ingridients)
            {
                list_ingredients.Items.Add(ingridient);
            }
        }
        public void Render_Orders()
        {
            list_orders.Items.Clear();
            /*MySqlDataReader orders_data = mainWindow.Query("SELECT `orders`.id, `users`.surname, `users`.name, `users`.patronymic, `dish`.name, `orders`.status, `size`.price " +
                "FROM `orders` " +
                "INNER JOIN `users` on(`orders`.client = `users`.id) " +
                "INNER JOIN `size` ON(`orders`.size = `size`.id) " +
                "INNER JOIN `dish` ON(`size`.id_dish = `dish`.id)");
            orders.Clear();
            if (orders_data == null) return;
            while (orders_data.Read())
            {
                Order order = new Order();
                order.id = Convert.ToInt32(orders_data.GetValue(0).ToString());
                order.user = String.Format("{0} {1} {2}", 
                    orders_data.GetValue(1).ToString(), orders_data.GetValue(2).ToString(), orders_data.GetValue(3).ToString());
                order.name_pizz = orders_data.GetValue(4).ToString();
                order.status = orders_data.GetValue(5).ToString();
                order.price = Convert.ToInt32(orders_data.GetValue(6).ToString());

                orders.Add(order);
            }
            foreach(Order order in orders)
            {
                list_orders.Items.Add(order);
            }*/
            IConnectionWork orders_data = mainWindow.IQuery("SELECT `orders`.id, `users`.surname, `users`.name, `users`.patronymic, `dish`.name, `orders`.status, `size`.price " +
                "FROM `orders` " +
                "INNER JOIN `users` on(`orders`.client = `users`.id) " +
                "INNER JOIN `size` ON(`orders`.size = `size`.id) " +
                "INNER JOIN `dish` ON(`size`.id_dish = `dish`.id)");
            orders.Clear();
            if (orders_data == null) return;
            while (orders_data.mySqlDataReader.Read())
            {
                Order order = new Order();
                order.id = Convert.ToInt32(orders_data.mySqlDataReader.GetValue(0).ToString());
                order.user = String.Format("{0} {1} {2}",
                    orders_data.mySqlDataReader.GetValue(1).ToString(), orders_data.mySqlDataReader.GetValue(2).ToString(), orders_data.mySqlDataReader.GetValue(3).ToString());
                order.name_pizz = orders_data.mySqlDataReader.GetValue(4).ToString();
                order.status = orders_data.mySqlDataReader.GetValue(5).ToString();
                order.price = Convert.ToInt32(orders_data.mySqlDataReader.GetValue(6).ToString());

                orders.Add(order);
            }
            foreach (Order order in orders)
            {
                list_orders.Items.Add(order);
            }
            orders_data.mySqlConnection.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.authorization);
        }

        private void Name_search(object sender, TextChangedEventArgs e)
        {
            List<User> users_temp = users_arr.FindAll(x => (String.Format("{0} {1} {2}",x.first_name.ToLower(), x.name.ToLower(), x.last_name.ToLower()).Contains(name.Text.ToLower())));
            list_users.Items.Clear();
            foreach (User user in users_temp)
            {
                list_users.Items.Add(user);
            }
        }

        private void list_users_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list_temp = (ListView)sender;
            User user = list_temp.SelectedItem as User;
            mainWindow.OpenPage(MainWindow.pages.user_change, user);
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.add_user);
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if(list_users.SelectedIndex != -1)
            {
                User user = list_users.SelectedItem as User;
                if (MessageBox.Show("Удалить пользователя: " + user.id.ToString(), "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    mainWindow.Query(String.Format("DELETE FROM `users` WHERE id = '{0}';", user.id));
                    users_arr.Clear();
                    Render_Users();
                }
                else
                {
                    if(MessageBox.Show("Удалить телефон: " + user.phone, "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        mainWindow.Query(String.Format("DELETE FROM `user_phone` WHERE `user_id` = '{0}' AND `phone` = '{1}';", user.id, user.phone));
                        users_arr.Clear();
                        Render_Users();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выделите элемент, который хотите удалить", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void ExportUser(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if(saveFileDialog.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
                foreach(User user in users_arr)
                {
                    sw.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                        user.id, user.first_name, user.name, user.last_name, user.phone, user.mail, user.street,
                        user.house, user.room, user.porch, user.floor, user.date, user.login, user.password, user.role));
                }
                sw.Close();
            }
        }

        private void ImportUser(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            if(openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                list_users.Items.Clear();
                users_arr.Clear();

                while (!sr.EndOfStream)
                {
                    string[] user_data = sr.ReadLine().Split('|');
                    User user = new User();
                    user.id = Convert.ToInt32(user_data[0]);
                    user.first_name = user_data[1];
                    user.name = user_data[2];
                    user.last_name = user_data[3];
                    user.phone = user_data[4];
                    user.mail = user_data[5];
                    user.street = user_data[6];
                    user.house = user_data[7];
                    user.room = user_data[8];
                    user.porch = user_data[9];
                    user.floor = user_data[10];
                    user.date = user_data[11];
                    user.login = user_data[12];
                    user.password = user_data[13];
                    user.role = Convert.ToInt32(user_data[14]);
                    users_arr.Add(user);
                }
                sr.Close();
                foreach (User user in users_arr)
                {
                    list_users.Items.Add(user);
                }
            }
        }

        private void AddDish(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.add_dish);
        }

        private void Dish_search(object sender, TextChangedEventArgs e)
        {
            List<Dish> dish_temp = dishes.FindAll(x => (x.name.ToLower().Contains(dish_serch_text.Text.ToLower())));
            parrent_dish.Children.Clear();
            foreach (Dish dish in dish_temp)
            {
                parrent_dish.Children.Add(new Elements.dish_itm(mainWindow, dish));
            }
        }

        private void Ingridients_search(object sender, TextChangedEventArgs e)
        {
            List<Ingridient> ingrid_temp = ingridients.FindAll(x => (x.name.ToLower()).Contains(ingr_search.Text.ToLower()));
            list_ingredients.Items.Clear();
            foreach (Ingridient ingrid in ingrid_temp)
            {
                list_ingredients.Items.Add(ingrid);
            }
        }

        private void list_ingrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list_temp = (ListView)sender;
            Ingridient ingrid = list_temp.SelectedItem as Ingridient;
            mainWindow.OpenPage(MainWindow.pages.ingrid_change, null, ingrid);
        }

        private void AddIngridient(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.add_ingridient);
        }

        private void DeleteIngridient(object sender, RoutedEventArgs e)
        {
            if(list_ingredients.SelectedIndex != -1)
            {
                Ingridient ingrid = list_ingredients.SelectedItem as Ingridient;
                if (MessageBox.Show("Удалить игредиент: " + ingrid.id.ToString(), "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    mainWindow.Query(String.Format("DELETE FROM `ingridient` WHERE id = '{0}';", ingrid.id));
                    ingridients.Clear();
                    Render_Ingrid();
                }
            }
            else
            {
                MessageBox.Show("Выделите элемент, который хотите удалить", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void ExportIngridient(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
                foreach (Ingridient ingrid in ingridients)
                {
                    sw.WriteLine(String.Format("{0}|{1}|{2}|{3}",
                        ingrid.id, ingrid.name, ingrid.weight, ingrid.price));
                }
                sw.Close();
            }
        }

        private void ImportIngridient(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                list_ingredients.Items.Clear();
                ingridients.Clear();

                while (!sr.EndOfStream)
                {
                    string[] ingrid_data = sr.ReadLine().Split('|');
                    Ingridient ingrid = new Ingridient();
                    ingrid.id = Convert.ToInt32(ingrid_data[0]);
                    ingrid.name = ingrid_data[1];
                    ingrid.weight = ingrid_data[2];
                    ingrid.price = ingrid_data[3];
                    ingridients.Add(ingrid);
                }
                sr.Close();
                foreach (Ingridient ingrid in ingridients)
                {
                    list_ingredients.Items.Add(ingrid);
                }
            }
        }

        private void Orders_search(object sender, TextChangedEventArgs e)
        {
            List<Order> orders_temp = orders.FindAll(x => (x.user.ToLower()).Contains(find_ord.Text.ToLower()));
            list_orders.Items.Clear();
            foreach(Order order in orders_temp)
            {
                list_orders.Items.Add(order);
            }
        }

        private void Press_orders(object sender, RoutedEventArgs e)
        {
            Button list_temp = (Button)sender;
            /*switch(orders.Find(x => x.id == Convert.ToInt32(list_temp.Tag.ToString())).status)
            {
                case "Неготов":
                    mainWindow.Query(String.Format("UPDATE `orders` SET `status` = 'Готовится' WHERE id = {0};", list_temp.Tag));
                    break;
                case "Готовится":
                    mainWindow.Query(String.Format("UPDATE `orders` SET `status` = 'В пути' WHERE id = {0};", list_temp.Tag));
                    break;
                case "В пути":
                    mainWindow.Query(String.Format("UPDATE `orders` SET `status` = 'Доставлен' WHERE id = {0};", list_temp.Tag));
                    break;
                case "Доставлен":
                    mainWindow.Query(String.Format("UPDATE `orders` SET `status` = 'Готовится' WHERE id = {0};", list_temp.Tag));
                    break;
            }*/
            switch (orders.Find(x => x.id == Convert.ToInt32(list_temp.Tag.ToString())).status)
            {
                case "Неготов":
                    IConnectionWork data = mainWindow.IQuery(String.Format("UPDATE `orders` SET `status` = 'Готовится' WHERE id = {0};", list_temp.Tag));
                    data.mySqlConnection.Close();
                    break;
                case "Готовится":
                    data = mainWindow.IQuery(String.Format("UPDATE `orders` SET `status` = 'В пути' WHERE id = {0};", list_temp.Tag));
                    data.mySqlConnection.Close();
                    break;
                case "В пути":
                    data = mainWindow.IQuery(String.Format("UPDATE `orders` SET `status` = 'Доставлен' WHERE id = {0};", list_temp.Tag));
                    data.mySqlConnection.Close();
                    break;
                case "Доставлен":
                    data = mainWindow.IQuery(String.Format("UPDATE `orders` SET `status` = 'Готовится' WHERE id = {0};", list_temp.Tag));
                    data.mySqlConnection.Close();
                    break;
            }
            new ToastContentBuilder()
                    .AddArgument("conversationId", 9813)
                    .AddText("#ХочуПиццу")
                    .AddText("Статус успешно изменен!")
                    .Show();
            Render_Orders();
        }

    }
}
