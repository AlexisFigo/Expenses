using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ProfilePageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Profile";
        }
    }
}
