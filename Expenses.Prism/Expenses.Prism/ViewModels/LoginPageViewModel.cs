using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;
        public LoginPageViewModel(INavigationService navigationService, IApiService apiService)
           : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            IsEnabled = true;
        }

        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));


        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void ForgotPasswordAsync()
        {
            //await _navigationService.NavigateAsync(nameof(RememberPasswordPage));
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);
                return;
            }

            //IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            //bool connection = await _apiService.CheckConnectionAsync(url);
            //if (!connection)
            //{
                //IsRunning = true;
                //IsEnabled = false;
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                //return;
            //}

            LoginRequest request = new LoginRequest
            {
                Password = Password,
                Username = Email,
                RememberMe = false,
                CultureInfo = "es"
            };

            Response response = await _apiService.Login(url, "api", "/Account/Login", request);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                //Password = string.Empty;
                return;
            }
            UserResponse user = (UserResponse)response.Result;
            //UserResponse user = JsonConvert.DeserializeObject<UserResponse>(response.Result.ToString());
            
            Settings.User = JsonConvert.SerializeObject(user);
            Settings.Token = user.Token;
            Settings.Id = user.Id;
            //IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/ExpensesMasterDetailPage/NavigationPage/TripsPage");
        }

        private async void RegisterAsync()
        {
            //await _navigationService.NavigateAsync(nameof(RegisterPage));
        }
    }
}
