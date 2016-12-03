﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (PlayerPosition) value;
            return position == PlayerPosition.NonAssigned ? "n/a" : position.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return PlayerPosition.NonAssigned;
            }
            else
            {
                return (PlayerPosition) value;
            }
        }
    }
}
