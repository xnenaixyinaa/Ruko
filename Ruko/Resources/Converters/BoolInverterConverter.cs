﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Resources
{
    /// <summary>
    /// true : false - false : true
    /// </summary>
    public class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}