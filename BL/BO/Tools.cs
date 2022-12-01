namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (var item in t.GetType().GetProperties())
        {
            if(item is List<T> && item != null)
            {
                str += "\n" + item.Name + ": ";
                List<T> mylist = item as List<T>;
                foreach (var obj in mylist)
                {
                    str += obj+" ";
                }
            }
            else 
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
        }
         return str;

    }
}
