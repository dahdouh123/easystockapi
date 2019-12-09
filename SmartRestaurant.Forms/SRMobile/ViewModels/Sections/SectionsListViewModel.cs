﻿using SmartRestaurant.Diner.Infrastructures;
using SmartRestaurant.Diner.Models;
using SmartRestaurant.Diner.Resources;
using SmartRestaurant.Diner.Services;
using SmartRestaurant.Diner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartRestaurant.Diner.ViewModels.Sections
{
    
    /// <summary>
    /// Used to bind Sections List to the view.
    /// </summary>
    public class SectionsListViewModel: SimpleViewModel
    {
        public static SectionsListViewModel Instance { get; set; }
        /// <summary>
        /// The list of section to be binded to the List control.
        /// </summary>
        public IList<SectionViewModel> Sections { get; set; }


        /// <summary>
        /// Constructor to fill the list of section from the service class
        /// </summary>
        public SectionsListViewModel()
        {
            
            ObservableCollection<SectionModel> listSections = SectionsService.GetListSections();
            Sections = new ObservableCollection<SectionViewModel>();
            foreach (var item in listSections)
            {
                Sections.Add(new SectionViewModel(item));
            }
            Instance = this;
        }
        /// <summary>
        /// Used to manage text orientation from right to left or from left to right accorging the langage selected.
        /// </summary>
        public TextAlignment OrientationText
        {
            get
            {
                if (AppResources.Culture != null)
                {
                    return AppResources.Culture.Name == "ar" ? TextAlignment.End : TextAlignment.Start;
                }
                else
                {
                    return TextAlignment.Start;
                }
            }
        }
        public FlowDirection FlowDirectionValue
        {

            get
            {
                if (AppResources.Culture != null)
                {
                    return AppResources.Culture.Name == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                }
                else
                {
                    return FlowDirection.LeftToRight;
                }
            }
        }
         private List<DishViewModel> selectedDishes;
         public List<DishViewModel> SelectedDishes
        {
            get
            {
                if (selectedDishes == null)
                    selectedDishes = new List<DishViewModel>();
                
                return selectedDishes;
            }
            set
            {
                selectedDishes = value;
                TotalPrice= SelectedDishes.Sum(d => d.Price);
                RaisePropertyChanged();

            }
        }

         public float TotalPrice
        {
            get { return SelectedDishes.Sum(d => d.Price); }
            set {
                if(TotalPrice!=value)
                    TotalPrice = value;
                    RaisePropertyChanged();
                
            }

        }

    }
}
