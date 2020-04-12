using Expenses.Common.Models;
using FFImageLoading.Work;
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
        private ImageSource _image;
        private bool _isRunning;
        private bool _isEnabled;
        private AddDetailsRequest _addDetailsRequest;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _addDetailCommand;
        public AddTripDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Add detail";
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand AddDetailCommand => _addDetailCommand ?? (_addDetailCommand = new DelegateCommand(AddDetailAsync));

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public AddDetailsRequest Detail
        {
            get => _addDetailsRequest;
            set => SetProperty(ref _addDetailsRequest, value);
        }

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

        private void ChangeImageAsync()
        {
            throw new NotImplementedException();
        }

        private void AddDetailAsync()
        {
            throw new NotImplementedException();
        }
    }
}
