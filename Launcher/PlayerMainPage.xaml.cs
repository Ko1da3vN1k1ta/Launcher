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
    /// Логика взаимодействия для PlayerMainPage.xaml
    /// </summary>
    public partial class PlayerMainPage : Page
    {
        private string login;
        private string region;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=MMORPG_Launcher_DB; Integrated Security=True");

        public PlayerMainPage(string login, string region)
        {
            InitializeComponent();
            this.login = login;
            this.region = region;

            LoadServers();
        }

        private void LoadServers()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ServerName FROM Servers WHERE Region = @Region", conn);
            cmd.Parameters.AddWithValue("@Region", region);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cbServers.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            

            if (cbServers != null)
            {
                
                MessageBox.Show("Игра запускается...");
                Application.Current.Shutdown();
            }
            else 
            {
                string selectedServer = cbServers.SelectedItem.ToString();

                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LoginHistory (Login, Region, Server, LoginTime) VALUES (@Login, @Region, @Server, @LoginTime)", conn);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Region", region);
                cmd.Parameters.AddWithValue("@Server", selectedServer);
                cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Пожалуйста, выбирите сервер");
            }
        }
    }
}


