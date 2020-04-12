using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class RememberPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private string _email;
        private DelegateCommand _recoverCommand;
        public RememberPasswordPageViewModel(INavigationService navigationService,IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Remember password";
            IsEnabled = true;
        }
        public DelegateCommand RecoverCommand => _recoverCommand ?? (_recoverCommand = new DelegateCommand(RecoverPasswordAsync));

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private async void RecoverPasswordAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return;
            }

            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();

            var request = new RecoverPasswordRequest
            {
                Email = this.Email,
                CultureInfo = "es"
            };
            string token = Settings.Token;
            Response response = await _apiService.PostAsync(url, "api", "/Account/RecoverPassword", request,token);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                //Password = string.Empty;
                return;
            }
            //mostrar mensaje aca
            await _navigationService.GoBackAsync();
        }
    }
}
