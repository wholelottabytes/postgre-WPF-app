﻿using beatsloves.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace beatsloves.Services
{
    public class PurchaseButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int userId && (userId == Session.UserID || Session.RoleID == 1))
            {
                return Visibility.Collapsed;
            }
            return 
            Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
