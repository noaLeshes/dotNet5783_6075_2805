
using System.Reflection.Metadata.Ecma335;

namespace BO;

[Serializable]
public class BlMissingEntityException : Exception
{
    public BlMissingEntityException(string message, Exception innerException)
        : base(message, innerException) { }
    public override string ToString()
    {
        return base.ToString() + $"Missing Entity";
    }
}

[Serializable]
public class BlAlreadyExistsEntityException : Exception
{
    public BlAlreadyExistsEntityException(string message)
        : base(message) { }
    public BlAlreadyExistsEntityException(string message, Exception innerException)
        : base(message, innerException) { }
    public override string ToString()
    {
        return base.ToString() + $"Entity is already exists";
    }
}

public class BlNullPropertyException : Exception
{
    public string MyName;
    public BlNullPropertyException(string name)
        : base() { MyName = name; }
    public BlNullPropertyException(string name, string massage)
        : base(massage) { MyName = name; }
    public BlNullPropertyException(string name, string massage, Exception innerException)
        : base(massage, innerException) { MyName = name; }
    public override string ToString()
    {
        return $"{MyName} is null!";
    }
        
}

[Serializable]
public class BlIncorrectDateException : Exception
{
    public string DateName;
    //public string State;
    public BlIncorrectDateException(string name/*, string state*/)
        : base() { DateName = name; /*State = state; */}
    public BlIncorrectDateException(string name/*, string state*/, string massage)
        : base(massage) { DateName = name; /*State = state;*/ }
    public BlIncorrectDateException(string name, string state, string massage, Exception innerException)
        : base(massage, innerException) { DateName = name; /*State = state;*/ }
    public override string ToString()
    {
        //if (State == "current")
        //    return $"{DateName} is already updated!";
        //else if (State == "previous")
        //    return $"Ship date isn't updated yet!";
        //else
            return "Error in date";
    }
}

[Serializable]
public class BlWrongCategoryException
: Exception
{
    public BlWrongCategoryException()
     : base() { }
    public BlWrongCategoryException(string massage)
        : base(massage) { }
    public BlWrongCategoryException(string massage, Exception innerException)
        : base(massage, innerException) { }
    public override string ToString()
    {
        return base.ToString();//מה זה אומר????????????
    }
}


///////// אולי להוסיף עוד פונקציות לפח חריגות שיש לנו