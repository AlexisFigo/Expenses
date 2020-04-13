using Expenses.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Expenses.Prism.ViewModels
{
    public class ExpensesMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ExpensesMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    //Icon = "tournament",
                    PageName = "ProfilePage",
                    Title = "Modify profile"
                },
                new Menu
                {
                    //Icon = "tournament",
                    PageName = "ChangePasswordPage",
                    Title = "Change password"
                },
                new Menu
                {
                    //Icon = "prediction",
                    PageName = "TripsPage",
                    Title = "Trips"
                },
                new Menu
                {
                    //Icon = "medal",
                    PageName = "AddTripPages",
                    Title = "Add trip"
                },
                new Menu
                {
                    //Icon = "user",
                    PageName = "logoutPages",
                    Title = "Logout"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }

    }
}
