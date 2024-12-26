using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Launcher
{
    public partial class AdminPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=MMORPG_Launcher_DB; Integrated Security=True");

        public AdminPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, FullName, Region, Login, IsBlocked FROM Players", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgUsers.ItemsSource = dt.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}");
            }
        }

        private void RefreshUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }

            DataRowView selectedRow = (DataRowView)dgUsers.SelectedItem;
            int userId = Convert.ToInt32(selectedRow["ID"]);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Players WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", userId);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Пользователь успешно удален.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления пользователя: {ex.Message}");
            }
        }

        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для блокировки.");
                return;
            }

            DataRowView selectedRow = (DataRowView)dgUsers.SelectedItem;
            int userId = Convert.ToInt32(selectedRow["ID"]);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Players SET IsBlocked = 1 WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", userId);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Пользователь успешно заблокирован.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка блокировки пользователя: {ex.Message}");
            }
        }

        private void UnblockUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для разблокировки.");
                return;
            }

            DataRowView selectedRow = (DataRowView)dgUsers.SelectedItem;
            int userId = Convert.ToInt32(selectedRow["ID"]);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Players SET IsBlocked = 0 WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", userId);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Пользователь успешно разблокирован.");
                LoadUsers();
            }
            catch (Exception ex)
            {

                
MessageBox.Show($"Ошибка разблокировки пользователя: {ex.Message}");
            }
        }

        private void ViewLoginStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT Server, COUNT(*) AS LoginCount FROM LoginHistory GROUP BY Server", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                string stats = "Статистика входов на серверы:\n";
                while (reader.Read())
                {
                    stats += $"Сервер: {reader["Server"]}, Количество входов: {reader["LoginCount"]}\n";
                }
                conn.Close();

                MessageBox.Show(stats, "Статистика входов");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка просмотра статистики: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}


