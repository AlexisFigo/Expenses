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
        private readonly INavigationService _navigationService;
        public AddTripPagesViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add trips";
        }
    }
}

