using Expenses.Common.Models;
using Expenses.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class TripDetailsPagesViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private TripResponse _trip;
        private List<TripDetailsResponse> _tripsDetails;
        private DelegateCommand _addDetailCommand;
        public TripDetailsPagesViewModel(INavigationService navigationService):base(navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand AddDetailCommand => _addDetailCommand ??
           (_addDetailCommand = new DelegateCommand(AddTripDetailAsync));

        public List<TripDetailsResponse> TripsDetails
        {
            get => _tripsDetails;
            set => SetProperty(ref _tripsDetails, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _trip = parameters.GetValue<TripResponse>("trip");
            Title = "Details";
            LoadDetails();
        }

        private void LoadDetails()
        {
            TripsDetails = _trip.TripDetails;
        }

        private async void AddTripDetailAsync()
        {
            var parameters = new NavigationParameters
            {
                { "trip", _trip.Id }
            };

            await _navigationService.NavigateAsync(nameof(AddTripDetailsPage), parameters);
        }
    }
}
