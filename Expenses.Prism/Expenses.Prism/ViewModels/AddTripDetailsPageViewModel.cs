using Expenses.Common.Helpers;
using Expenses.Common.Models;
using Expenses.Common.Services;
using FFImageLoading.Work;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Expenses.Prism.ViewModels
{
    public class AddTripDetailsPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private Xamarin.Forms.ImageSource _image;
        private MediaFile _file;
        private bool _isRunning;
        private bool _isEnabled;
        private string _tripId;
        private AddDetailsRequest _addDetailsRequest;
        private ExpensesTypeResponse _expensesType;
        private ObservableCollection<ExpensesTypeResponse> _expensesTypes;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _addDetailCommand;
        public AddTripDetailsPageViewModel(INavigationService navigationService, 
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = "Add detail";
            IsEnabled = true;
            Detail = new AddDetailsRequest();
            LoadExpensesTypeAsync();
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand AddDetailCommand => _addDetailCommand ?? (_addDetailCommand = new DelegateCommand(AddDetailAsync));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tripId = parameters.GetValue<string>("tripId");
        }
        public Xamarin.Forms.ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ExpensesTypeResponse ExpensesType 
        {
            get => _expensesType;
            set => SetProperty(ref _expensesType, value);
        }

        public ObservableCollection<ExpensesTypeResponse> ExpensesTypes
        {
            get => _expensesTypes;
            set => SetProperty(ref _expensesTypes, value);
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

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                "¿Donde quieres tomar la foto",
                "Cancelar",
                null,
                "Galeria",
                "Camara");

            if (source == "Cancelar")
            {
                _file = null;
                return;
            }

            if (source == "Camara")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = Xamarin.Forms.ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async void AddDetailAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            //bool connection = await _apiService.CheckConnectionAsync(url);
            //if (!connection)
            //{
            //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
            //return;
            //}
            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            var reques = new AddDetailsRequest
            {
                ExpensesTypeId  = ExpensesType.Id.ToString(),
                Date = Detail.Date,
                Cost = Detail.Cost,
                VoucherPath = imageArray,
                TripId = _tripId,
                CultureInfo = "es"

            };
            string token = Settings.Token;

            Response response = await _apiService.PostAsync(url, "api", "/Trip/AddDetails", reques,token);

            if (!response.IsSuccess)
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            //await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        private async void LoadExpensesTypeAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            //bool connection = await _apiService.CheckConnectionAsync(url);
            //if (!connection)
            //{
            //await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
            //return;
            //}
            Response response = await _apiService.GetComboBox<ExpensesTypeResponse>(url, "api", "/Trip/GetExpenesesType");

            if (!response.IsSuccess)
            {
                //await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<ExpensesTypeResponse> list = (List<ExpensesTypeResponse>)response.Result;
            ExpensesTypes = new ObservableCollection<ExpensesTypeResponse>(list.OrderBy(t => t.Name));
        }
    }
}
