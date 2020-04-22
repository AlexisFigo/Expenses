using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Expenses.Prism.Helpers;
using Expenses.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace Expenses.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private bool _isRunning;
        private DelegateCommand _change;
        private UserRequest _user;
        public ProfilePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = Languages.ModifyUser;
            _apiService = apiService;
            _navigationService = navigationService;
            IsEnabled = true;
            LoadUser();
        }


        public DelegateCommand ChangeCommand => _change ?? (_change = new DelegateCommand(ChangeAsync));

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void ChangeAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            User.CultureInfo = Languages.Culture;
            string token = Settings.Token;

            Response response = await _apiService.PutAsync(url, "api", "/Account/EditUser", User, token);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                return;
            }
            IsEnabled = true;
            IsRunning = false;
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.LoginError, Languages.Accept);
            await _navigationService.NavigateAsync(nameof(TripsPage));
        }

        private void LoadUser()
        {
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            User = new UserRequest
            {
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumenError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
