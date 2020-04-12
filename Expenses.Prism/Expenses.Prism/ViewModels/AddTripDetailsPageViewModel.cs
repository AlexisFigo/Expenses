using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class AddTripDetailsPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        public AddTripDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Add detail";
        }
    }
}
