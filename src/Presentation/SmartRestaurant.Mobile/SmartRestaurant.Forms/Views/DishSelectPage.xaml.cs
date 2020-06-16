﻿using SmartRestaurant.Diner.CustomControls;
using SmartRestaurant.Diner.ViewModels.Sections;
using SmartRestaurant.Diner.ViewModels.Sections.Subsections.Currencies.Currencies;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartRestaurant.Diner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishSelectPage : ContentPage
    {

        public DishSelectPage(DishViewModel _model)
        {
            InitializeComponent();
            BindingContext = _model;
            viewmodel = (DishViewModel)BindingContext;
            
        }
        public DishViewModel viewmodel { get; private set; }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RadioOption;

            if (item == null)
                return;

            foreach (var group in ((DishViewModel)BindingContext).Specifications.Specifications)
            {
                if (group.RadioOptionsVM.Contains(item))
                {
                    foreach (var s in group.RadioOptionsVM.Where(x => x.IsSelected))
                    {
                        s.IsSelected = false;
                    }

                    item.IsSelected = true;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = ((Grid)sender).BindingContext as RadioOption;

            if (item == null)
                return;

            foreach (var group in ((DishViewModel)BindingContext).Specifications.Specifications)
            {
                if (group.RadioOptionsVM.Contains(item))
                {
                    foreach (var s in group.RadioOptionsVM.Where(x => x.IsSelected))
                    {
                        s.IsSelected = false;
                    }

                    item.IsSelected = true;
                }
            }
        }
        private bool _ShowCurrencies;
        public bool ShowCurrencies
        {
            get
            {
                return _ShowCurrencies;
            }
            set
            {
                _ShowCurrencies = value;
                OnPropertyChanged("ShowCurrencies");
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShowCurrencies = !ShowCurrencies;
        }

        private void Curlv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            ((DishViewModel)BindingContext).Currency = (CurrencyViewModel)(e.SelectedItem);
            ShowCurrencies = false;
        }

        private void Curlv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((DishViewModel)BindingContext).Currency = (CurrencyViewModel)(e.Item);
            ShowCurrencies = false;
        }
    }
}