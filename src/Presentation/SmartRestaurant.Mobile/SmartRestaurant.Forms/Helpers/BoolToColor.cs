﻿using System;
using Xamarin.Forms;

namespace SmartRestaurant.Diner.Helpers
{
    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return Color.FromHex("#01A9AC");
            return Color.White;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return Color.Red;
            return Color.White;
        }
    }
}
