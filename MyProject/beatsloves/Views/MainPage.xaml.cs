using beatsloves.ViewModels;
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
using beatsloves.Navigation;
using System.Windows.Shapes;
using System.Reflection;


namespace beatsloves.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(NavigationService navigationService)
        {
            InitializeComponent();
            this.Loaded += (s, e) => MainPage_Loaded(navigationService);
        }

        private void MainPage_Loaded(NavigationService navigationService)
        {
            
            var navService = System.Windows.Navigation.NavigationService.GetNavigationService(this);
            if (navService == null)
            {
                throw new InvalidOperationException("Navigation service is not available.");
            }

            MainViewModel viewModel = new MainViewModel(navigationService);
            this.DataContext = viewModel;

            viewModel.LoadBeatsCommand.Execute(null);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var navService = System.Windows.Navigation.NavigationService.GetNavigationService(this);
            if (navService != null && navService.CanGoBack)
            {
                navService.GoBack();
            }
        }

    }
}
