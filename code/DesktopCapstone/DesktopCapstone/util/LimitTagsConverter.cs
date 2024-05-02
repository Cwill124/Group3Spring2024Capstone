using BibtexLibrary.Parser.Nodes;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopCapstone.util
{
    public class LimitTagsConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<Tags> tags))
            {
                return value;
            }
            int maxTags;
            if (!int.TryParse(parameter?.ToString(), out maxTags)) {
                maxTags = 3;
            }
            return tags.Take(maxTags).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
