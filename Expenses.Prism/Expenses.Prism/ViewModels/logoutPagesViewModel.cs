using Expenses.Common.Helpers;
using Expenses.Prism.Views;
using Prism.Navigation;

namespace Expenses.Prism.ViewModels
{
    public class logoutPagesViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public logoutPagesViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Logout";
            _navigationService = navigationService;
            Logout();
        }

        private async void Logout()
        {
            Settings.User = null;
            Settings.Token = null;
            Settings.Id = null;
            Settings.Document = null;
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
    }
}
