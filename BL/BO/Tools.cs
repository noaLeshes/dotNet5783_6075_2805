﻿using System.Reflection;

namespace BO;
 
static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            str += "\n" + item.Name + ": ";
            if (item.GetValue(t,null) is IEnumerable<object>)
            {
                IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj:t, null);
                string s = String.Join(" ", list);
                str += s;
            }
            else 
                str += item.GetValue(t, null);
        }
         return str + "\n";

    }
}
