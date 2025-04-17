using beatsloves.ViewModels;
using beatsloves.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace beatsloves.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            this.Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationService = System.Windows.Navigation.NavigationService.GetNavigationService(this);
            if (navigationService == null)
            {
                throw new InvalidOperationException("Navigation service is not available.");
            }
            this.DataContext = new LoginViewModel(new NavigationService(navigationService));
            UsernameTextBox.Focus();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordBox.Focus(); 
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton.Focus(); 
                LoginButton.Command.Execute(null); 
            }
        }

     
    }
}
