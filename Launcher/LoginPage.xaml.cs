using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=MMORPG_Launcher_DB; Integrated Security=True");

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            string region = (cbRegion.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(region))
            {
                MessageBox.Show("Пожалуйста, выберите регион.");
                return;
            }
            if (login == "admin" && password == "admin123") 
            {
                conn.Close();
                NavigationService.Navigate(new AdminPage());
                return;
            }
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Players WHERE Login = @Login AND Password = @Password", conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                bool isBlocked = reader.GetBoolean(reader.GetOrdinal("IsBlocked"));
                if (isBlocked)
                {
                    MessageBox.Show("Ваш аккаунт заблокирован!");
                    conn.Close();
                    return;
                }

                conn.Close();
                NavigationService.Navigate(new PlayerMainPage(login, region));
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
            conn.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}

