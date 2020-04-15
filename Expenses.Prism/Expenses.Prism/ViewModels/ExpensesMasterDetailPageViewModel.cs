using Expenses.Common.Models;
using Expenses.Prism.Helpers;
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
                    Icon = "ic_user",
                    PageName = "ProfilePage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "ic_password",
                    PageName = "ChangePasswordPage",
                    Title = Languages.ChangePassword
                },
                new Menu
                {
                    Icon = "ic_trip",
                    PageName = "TripsPage",
                    Title = Languages.Trips
                },
                new Menu
                {
                    Icon = "ic_detail",
                    PageName = "AddTripPages",
                    Title = Languages.AddTrip
                },
                new Menu
                {
                    Icon = "ic_logout",
                    PageName = "logoutPages",
                    Title = Languages.Logout
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
