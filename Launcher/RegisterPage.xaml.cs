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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=MMORPG_Launcher_DB; Integrated Security=True");

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string birthDate = dpBirthDate.SelectedDate?.ToString("yyyy-MM-dd");
            string gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
            string region = (cbRegion.SelectedItem as ComboBoxItem)?.Content.ToString();
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Players (FullName, BirthDate, Gender, Region, Login, Password) VALUES (@FullName, @BirthDate, @Gender, @Region, @Login, @Password)", conn);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@Region", region);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Password", password);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Регистрация прошла успешно!");
            NavigationService.Navigate(new LoginPage());
        }
    }
}
