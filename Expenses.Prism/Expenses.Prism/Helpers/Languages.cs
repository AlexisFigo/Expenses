using Expenses.Common.Interfaces;
using Expenses.Prism.Resources;
using Xamarin.Forms;

namespace Expenses.Prism.Helpers
{

    public static class Languages
    {
        static Languages()
        {
            System.Globalization.CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.ToString();
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static void SetCulture(int num)
        {
            System.Globalization.CultureInfo ci;
            switch (num)
            {
                case 0:
                case 1:
                    ci = new System.Globalization.CultureInfo("fr-FR");
                    break;
                case 2:
                case 3:
                    ci = new System.Globalization.CultureInfo("pt-BR");
                    break;
                case 4:
                case 5:
                    ci = new System.Globalization.CultureInfo("it-IT");
                    break;
                case 6:
                case 7:
                    ci = new System.Globalization.CultureInfo("de-DE");
                    break;
                case 8:
                case 9:
                    ci = new System.Globalization.CultureInfo("nl-NL");
                    break;
                default:
                    ci = new System.Globalization.CultureInfo("en-US");
                    break;
            }
        }

        public static string Culture { get; set; }

        public static string Open => Resource.Open;

        public static string Update => Resource.Update;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;

        public static string UserUpdated => Resource.UserUpdated;

        public static string Save => Resource.Save;

        public static string ChangePassword => Resource.ChangePassword;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string Ok => Resource.Ok;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string Logout => Resource.Logout;

        public static string LoginError => Resource.LoginError;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Name => Resource.Name;

        public static string Loading => Resource.Loading;

        public static string ModifyUser => Resource.ModifyUser;

        public static string Login => Resource.Login;

        public static string ExpensesType => Resource.ExpensesType;
        public static string Document => Resource.Document;

        public static string DocumentPlaceholder => Resource.DocumentPlaceholder;
        public static string StartDate => Resource.StartDate;
        public static string Cost => Resource.Cost;
        public static string CostPlaceholder => Resource.CostPlaceholder;
        public static string AddDetail => Resource.AddDetail;
        public static string City => Resource.City;
        public static string EnnDate => Resource.EndDate;
        public static string Description => Resource.Description;
        public static string DescriptionPlaceholder => Resource.DescriptionPlaceholder;
        public static string RememberPassword => Resource.RememberPassword;

        public static string RecoverPassword => Resource.RecoverPassword;

        public static string AddTrip => Resource.AddTrip;

        public static string Trips => Resource.Trips;
    }
}
