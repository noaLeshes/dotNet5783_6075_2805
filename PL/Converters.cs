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
                    return "White";

                if (value.ToString() != "☀️")
                    return "Black";
                else
                {
                    return "White";
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
                return "White";
            else
            {
                return "Black";

            }
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
