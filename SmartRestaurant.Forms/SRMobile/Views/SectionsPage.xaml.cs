﻿using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using IntelliAbb.Xamarin.Controls;
using SmartRestaurant.Diner.CustomControls;
using SmartRestaurant.Diner.ViewModels.DeseasesAllergies;
using SmartRestaurant.Diner.ViewModels.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace SmartRestaurant.Diner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionsPage : ContentPage
    {
        SectionsListViewModel model;

        /// <summary>
        /// Constructor to bind an object of type SectionsListViewModel to the context.
        /// </summary>
        /// <param name="_model"></param>
        public SectionsPage(SectionsListViewModel _model)
        {
            InitializeComponent();
            model = _model;
            BindingContext = model;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (SectionsListViewModel.Seats.Seats.Any(s => s.CurrentOrder.Lines == null ||
            s.CurrentOrder.Lines.Count == 0))
            {
                if (SectionsListViewModel.Seats.SelectedSeat.IlnessesAllergiesClicked)
                {
                    if (SectionsListViewModel.Seats.SelectedSeat.CurrentOrder.Lines != null && SectionsListViewModel.Seats.SelectedSeat.CurrentOrder.Lines.Count > 0)
                        await ((CustomNavigationPage)(App.Current.MainPage)).PushAsync(new DinerCommandRecap());
                }
                else
                {
                    await ((CustomNavigationPage)(App.Current.MainPage)).PushAsync(new DeseasesAllergiesPage(new DeseasesAllergiesListViewModel(
                        SectionsListViewModel.Seats.SelectedSeat
                        )));
                }
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new GlobalRecap(
                    SectionsListViewModel.Seats.SelectedSeat.Table
                    ));
            }
        }
    }
}