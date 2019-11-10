﻿using SmartRestaurant.Diner.Infrastructures;
using SmartRestaurant.Diner.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartRestaurant.Diner.ViewModels.Tables
{
    /// <summary>
    /// Used to manage tables as a ViewModel
    /// </summary>
    public class TablesViewModel: SimpleViewModel
    {
        public readonly TableModel table;

        /// <summary>
        /// Get the TableModel from the Model.
        /// </summary>
        /// <param name="_table"></param>
        public TablesViewModel(TableModel _table)
        {
            this.table = _table;
        }

        public int Id { get { return table.Id; } }

        public string Numero
        {
            get { return table.Numero; }
            set
            {
                if (table.Numero != value)
                {
                    table.Numero = value;
                    RaisePropertyChanged();
                }
            }
        }
        public short NombreChaises
        {
            get { return table.NombreChaises; }
            set
            {
                if (table.NombreChaises != value)
                {
                    table.NombreChaises = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int? ZoneId
        {
            get { return table.ZoneId; }
            set
            {
                if (table.ZoneId != value)
                {
                    table.ZoneId = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Used to indicate when a table is selected.
        /// </summary>
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged();
                    if (isSelected) BorderColor = Color.LightBlue;
                    else
                        BorderColor = Color.Transparent;
                    RaisePropertyChanged("BorderColor");
                }
            }
        }
        private Color bordercolor;
        public Color BorderColor
        {
            get
            {
                if (isSelected)
                    return Color.LightBlue;
                else
                    return Color.LightGray;
            }
            set
            {
                if (bordercolor != value)
                    bordercolor = value;
            }
        }
        private List<int> takenChairs;
        public List<int> TakenChairs
        {
            get
            {
                if (takenChairs == null)
                    takenChairs = new List<int>();
                return takenChairs;
            }
            
            set
            {
                

                    takenChairs = value;
                    if (takenChairs!=null && takenChairs.Count >0)
                    {

                    BackgroundColor = Color.LightBlue; }

                        else
                    BackgroundColor = Color.Transparent;
                        RaisePropertyChanged("BackgroundColor");
                   
                
            }
        }
        private Color backgroundcolor;
        public Color BackgroundColor
        {
            get
            {
                if (takenChairs != null && takenChairs.Count > 0)
                    return Color.Orange;
                else
                    return Color.Transparent;
            }
            set
            {
                if (backgroundcolor != value)

                    backgroundcolor = value;
            }
        }

    }
}
