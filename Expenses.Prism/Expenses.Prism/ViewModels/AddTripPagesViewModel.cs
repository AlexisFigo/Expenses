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
    public class AddTripPagesViewModel : ViewModelBase
    {
        private DelegateCommand _add;
        private bool _isEnabled;

        private readonly INavigationService _navigationService;
        private IApiService _apiService;
        public AddTripPagesViewModel(INavigationService navigationService,IApiService apiService) : base(navigationService)
        {
            Title = "Add trips";
            _apiService = apiService;
            IsEnabled = true;
        }

        public DelegateCommand AddCommand => _add ?? (_add = new DelegateCommand(AddTripSync));

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void AddTripSync()
        {
            if (string.IsNullOrEmpty(Description))
            {
                //mostrar mensaje de error
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            //bool connection = await _apiService.CheckConnectionAsync(url);
            //if (!connection)
            //{
            //IsRunning = true;
            //IsEnabled = false;
            //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
            //return;
            //}
            IsEnabled = false;

            CreateTripRequest request = new CreateTripRequest
            {
                UserId = Settings.Id,
                StartDate = StartDate,
                EndDate = EndDate,
                CityId = "021272ec-74a7-484b-a1ea-7ae921b74a05",
                Description = Description,
                CultureInfo = "es"
            };

            string token = Settings.Token;

            Response response = await _apiService.AddTtrip(url, "api", "/Trip/CreateTrip", request,token);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                //Password = string.Empty;
                return;
            }

            IsEnabled = true;
        }
    }
}

