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

namespace ЗАЯ.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regis.xaml
    /// </summary>
    public partial class Regis : Page
    {
        public Regis()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (username == "admin" && password == "admin123")
                {
                    MessageBox.Show("Администратор вошел в систему");
                    AddT addTPage = new AddT();
                    addTPage.IsAdmin = true;
                    this.NavigationService.Navigate(addTPage);
                }
                else
                {
                    MessageBox.Show("Пользователь вошел в систему");
                    AddT addTPage = new AddT();
                    addTPage.IsAdmin = false;
                    this.NavigationService.Navigate(addTPage);
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены. Проверьте поля логина и пароля.", "Предупреждение");
            }
        }
    }
}
