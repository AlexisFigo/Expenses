using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Expenses.Prism.Helpers;
using Expenses.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class AddTripPagesViewModel : ViewModelBase
    {
        private DelegateCommand _add;
        private bool _isEnabled;
        private bool _isRunning;

        private readonly INavigationService _navigationService;
        private IApiService _apiService;
        private CityResponse _city;
        private ObservableCollection<CityResponse> _cities;

        public AddTripPagesViewModel(INavigationService navigationService,IApiService apiService) : base(navigationService)
        {
            Title = "Add trips";
            _apiService = apiService;
            IsEnabled = true;
            LoadCities();
        }

        public DelegateCommand AddCommand => _add ?? (_add = new DelegateCommand(AddTripSync));

        public CityResponse City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<CityResponse> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

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

        private async void AddTripSync()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DescriptionError, Languages.Accept);
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {

                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            IsEnabled = false;
            IsRunning = true;
            CreateTripRequest request = new CreateTripRequest
            {
                UserId = Settings.Id,
                StartDate = StartDate,
                EndDate = EndDate,
                CityId = City.Id,
                Description = Description,
                CultureInfo = "es"
            };

            string token = Settings.Token;

            Response response = await _apiService.PostAsync(url, "api", "/Trip/CreateTrip", request,token);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            IsRunning = false;
            IsEnabled = true;

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.NavigateAsync(nameof(TripsPage));
        }

        private async void LoadCities()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            IsEnabled = false;
            IsRunning = true;

            string token = Settings.Token;

            Response response = await _apiService.GetComboBox<CounrtriesResponse>(url, "api", "/Trip/GetCountries");

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<CounrtriesResponse> counrtries = (List<CounrtriesResponse>)response.Result;
            Cities = new ObservableCollection<CityResponse>();
            foreach (var itm in counrtries)
            {
                foreach (var itmj in itm.Cities)
                {
                    Cities.Add(itmj);
                }
            }

            IsRunning = false;
            IsEnabled = true;
        }
    }
}

