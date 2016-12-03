﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerPositionListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var positions = (IEnumerable<PlayerPosition>) value;
            return positions.Select(playerPosition => 
            playerPosition == PlayerPosition.NonAssigned ? "n/a" : playerPosition.ToString()).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
