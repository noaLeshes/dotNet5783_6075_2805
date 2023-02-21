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
    public class NotDateTimeToVisibilityConverter : IValueConverter // convert for update bottons
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
                return Visibility.Hidden; //if the date is not null return hidden
            else
            {
                return Visibility.Visible;// else visible
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
        //convert if cart is empty the button is hidden else visible
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0")
                return Visibility.Visible; //
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class cartEmptyToTextConverter : IValueConverter
    {
        // change the text of the button acording to the total price
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0") // if cart is not empty 
                return "Continue Shopping"; 
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
        // change the color of the widow acording to the contant of the button
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return "#FF222A18";
            }
            if (value.ToString() != "☀️")
            {
                return "#FF97AD96";
            }
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
        // change the color of the window acording to the contant of the button opposite
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
       // if we enterd the window with empty name we want the border to be hidden
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
        //convert from name property type to id readOnly property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")//if null return true else false
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
        //convert from name property type to category IsHitTestVisible property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "") // if text is null return true else false
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
            {
                if (value.ToString()![0] == ' ') //get the name and if he has space in the begining he returns hidden

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
    public class VisibilityByMail : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mail = (string)value;
            if (mail == "example@gmail.com" || mail=="") // if we gor lowercase e we came from the button log 
                return Visibility.Hidden; //so we will return hidden to the grid
            else
            {
                return Visibility.Visible; // we came from sign
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
