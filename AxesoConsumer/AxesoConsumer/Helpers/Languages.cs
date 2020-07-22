
using AxesoConsumer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AxesoConsumer.Resources;
namespace AxesoConsumer.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }
        public static string RePasswordValidation
        {
            get { return Resource.RePasswordValidation; }
        }
        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string Email
        {
            get { return Resource.Email; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }


        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Information
        {
            get { return Resource.Information; }
        }


        public static string MyLanguages
        {
            get { return Resource.MyLanguages; }
        }
        public static string HaveAccount
        {
            get { return Resource.HaveAccount; }
        }
        public static string Agree
        {
            get { return Resource.Agree; }
        }

        public static string NameValidation {
            get { return Resource.NameValidation; }
        }
        public static string AdressValidation
        {
            get { return Resource.AdressValidation; }
        }
        public static string PhoneValidation
        {
            get { return Resource.PhoneValidation; }
        }
        public static string AgreeValidation
        {
            get { return Resource.AgreeValidation; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }
        public static string Success
        {
            get { return Resource.Success; }
        }
        public static string SuccessRegister
        {
            get { return Resource.SuccessRegister; }
        }
        public static string PasswordDontForget
        {
            get { return Resource.PasswordDontForget; }
        }
        public static string PasswordRecovery
        {
            get { return Resource.PasswordRecovery; }
        }
        public static string PasswordRecoveryText
        {
            get { return Resource.PasswordRecoveryText; }
        }
        public static string PasswordYourNew
        {
            get { return Resource.PasswordYourNew; }
        }
        public static string Recover
        {
            get { return Resource.Recover; }
        }

        public static string CurrentPassword
        {
            get { return Resource.CurrentPassword; }
        }
        public static string NewPassword
        {
            get { return Resource.NewPassword; }
        }
        public static string ConfirmNewPassword
        {
            get { return Resource.ConfirmNewPassword; }
        }
        public static string ChangePassword
        {
            get { return Resource.ChangePassword; }
        }
        public static string Modify
        {
            get { return Resource.Modify; }
        }
        public static string Ubications
        {
            get { return Resource.Ubications; }
        }
        public static string PasswordDiferent
        {
            get { return Resource.PasswordDiferent; }
        }
    }
}