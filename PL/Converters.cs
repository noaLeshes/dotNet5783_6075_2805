using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PL
{
    public class NotDateTimeToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
                return Visibility.Hidden; //Visibility.Collapsed;
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NotshipingToVisibilityConverter : IMultiValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0].ToString() == "" && value[1].ToString() == "")// not shippted
                return Visibility.Hidden; //Visibility.Collapsed;
            if (value[1].ToString() != "") // if already delivered
                return Visibility.Hidden;
            else
            {
                return Visibility.Visible;
            }
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class convertImagePathToBitmap : IValueConverter
    {
        //convert from string type to Bitmap type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string img = (string)value;
                string dirrectory = Environment.CurrentDirectory[..^4];
                string fullName = dirrectory + img;
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string img = @"\pics\IMGNotFound.jpg";
                string dirrectory = Environment.CurrentDirectory[..^4];
                string fullName = dirrectory + img;
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullName));
                return bitmapImage;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class cartEmptyConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0")
                return Visibility.Visible; //Visibility.Collapsed;
            else
            {
                return Visibility.Hidden;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class cartEmptyToTextConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0")
                return "Continue Shopping"; //Visibility.Collapsed;
            else
            {
                return "start Shopping";
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class backgroundConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                if(value == null)
                    return "#FF222A18";

            if (value.ToString() != "☀️")
                return "#FF97AD96";
            else
            {
                return "#FF222A18";
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class backgroundOpositeConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "☀️")
                return "#FF222A18";
            else
            {
                return "#FF97AD96";

            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class borderVisibility : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
                return Visibility.Visible;

            else
            {
              return Visibility.Hidden;

            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TextToBool : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
                return true;
            else
            {
                return false;

            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TextToBoolOpposite : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "")
                return true;
            else
            {
                return false;

            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class visibilToHidden : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
                return Visibility.Hidden;
            else
            {
                return Visibility.Visible;

            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NameTovisibility : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
            {
                if (value.ToString()[0] == ' ')
                    return Visibility.Hidden;
                else
                {
                    return Visibility.Visible;

                }
            }
            return Visibility.Visible;

        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class Converters
    {
    }
}
