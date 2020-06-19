﻿using SmartRestaurant.Diner.CustomControls;
using SmartRestaurant.Diner.ViewModels;
using SmartRestaurant.Diner.Views;
using Xamarin.Forms;

namespace SmartRestaurant.Diner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Launch the first Page.
            MainPage = new CustomNavigationPage(new PasswordPage(new PasswordViewModel()));
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
