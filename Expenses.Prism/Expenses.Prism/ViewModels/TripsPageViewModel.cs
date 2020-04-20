using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using Expenses.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private List<TripItemViewModel> _trips;
        public TripsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Trips";
            LoadTripsAsync();
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void LoadTripsAsync()
        {
            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TripRequest request = new TripRequest
            {
                Id = Settings.Id,
                CultureInfo = "es"
            };
            string token = Settings.Token;
            IsRunning = true;
            Response response = await _apiService.GetTrips<TripResponse>(url, 
                "api", 
                "/Trip/GetTrips",
                request,
                token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }

            List<TripResponse> tournaments = (List<TripResponse>)response.Result;
            Trips = tournaments.Select(t => new TripItemViewModel(_navigationService)
            {
               StartDate = t.StartDate,
               EndDate = t.EndDate,
               Id = t.Id,
               Description = t.Description,
               City = t.City,
               TripDetails = t.TripDetails
            }).ToList();
            IsRunning = false;
        }
    }
}
