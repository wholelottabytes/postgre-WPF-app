using beatsloves.Models;
using beatsloves.Navigation;
using beatsloves.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using beatsloves.Models;

namespace beatsloves.Views
{
    public partial class SoldBeatsPage : Page
    {
        private SoldBeatsPageViewModel _viewModel;

        public SoldBeatsPage()
        {
            InitializeComponent();

            _viewModel = new SoldBeatsPageViewModel();
            DataContext = _viewModel;
           
            Loaded += (s, e) => _viewModel.LoadUserBeatsSoldCommand.Execute(null);
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
