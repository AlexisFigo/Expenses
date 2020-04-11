using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class logoutPagesViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public logoutPagesViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Logout";
        }
    }
}
