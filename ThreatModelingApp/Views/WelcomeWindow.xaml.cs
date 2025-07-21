using System.Windows;
using System.Windows.Controls;

namespace ThreatModelingApp2.Views
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Тут можно добавить простую проверку (имитация)
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (!string.IsNullOrWhiteSpace(username))
            {
                // Переход к основному окну
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите имя пользователя");
            }
        }
    }
}
