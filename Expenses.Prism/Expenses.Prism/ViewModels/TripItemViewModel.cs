using Expenses.Common.Models;
using Expenses.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Prism.ViewModels
{
    public class TripItemViewModel : TripResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTripCommand;

        public TripItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTripCommand => _selectTripCommand ??
           (_selectTripCommand = new DelegateCommand(SelectTtripAsync));

        private async void SelectTtripAsync()
        {
            var parameters = new NavigationParameters
            {
                { "trip", this }
            };

            //Settings.Tournament = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(TripDetailsPages), parameters);
        }
    }
}
