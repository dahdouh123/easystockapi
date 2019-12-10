﻿using SmartRestaurant.Diner.ViewModels.Sections.Subsections.Ingredientes.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartRestaurant.Diner.Helpers
{
        public class MultiplyConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
            int w = 0;
            int.TryParse(((Span)parameter).Text,out w);
            return (int)value * w;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int w = 0;
            int.TryParse(((Span)parameter).Text, out w);
            return (int)value * w;
        }
        }    
}
