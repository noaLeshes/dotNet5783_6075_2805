﻿namespace BO;

static class Tools
{
    public static string ToStringProperty<T>>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            if(item.GetType() ==)
            str += "\n" + item.Name + ": " + item.GetValue(t, null);
        }
         return str;

    }
}
