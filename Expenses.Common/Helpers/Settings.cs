using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Helpers
{
    public static class Settings
    {
        private const string _user = "user";
        private const string _token = "token";
        private const string _id = "id";
        private const string _document = "document";
        private static readonly string _stringDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string User 
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Id
        {
            get => AppSettings.GetValueOrDefault(_id, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_id, value);
        }
        public static string Document
        {
            get => AppSettings.GetValueOrDefault(_document, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_document, value);
        }

    }
}
