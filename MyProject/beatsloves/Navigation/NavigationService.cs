using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace beatsloves.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly System.Windows.Navigation.NavigationService _navigationService;

        public NavigationService(System.Windows.Navigation.NavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public void NavigateTo(object destinationPage)
        {
            _navigationService.Navigate(destinationPage);
        }
    }
}

