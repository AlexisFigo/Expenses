using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IRegexHelper _regexHelper;
        private bool _isEnabled;
        private bool _isRunning;
        private DelegateCommand _register;
        private UserRequest _userRequest;
        public RegisterPageViewModel(INavigationService navigationService, IApiService apiService,
            IRegexHelper regexHelper)
           : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _regexHelper = regexHelper;
            Title = "Register";
            User = new UserRequest();
            IsEnabled = true;
        }

        public DelegateCommand RegisterCommand => _register ?? (_register = new DelegateCommand(RegisterAsync));

        public UserRequest User
        {
            get => _userRequest;
            set => SetProperty(ref _userRequest, value);
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

        private async void RegisterAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsEnabled = false;
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            User.CultureInfo = "es";
            string token = Settings.Token;

            Response response = await _apiService.PostAsync(url, "api", "/Account/CreateUser", User,token);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                //Password = string.Empty;
                return;
            }

            IsEnabled = true;
            IsRunning = false;
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
           
            if (string.IsNullOrEmpty(User.FirstName))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Email) || !_regexHelper.IsValidEmail(User.Email))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return false;
            }


            if (string.IsNullOrEmpty(User.Password) || User.Password?.Length < 6)
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.PasswordConfirm))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirmError1, Languages.Accept);
                return false;
            }

            if (User.Password != User.PasswordConfirm)
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirmError2, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
