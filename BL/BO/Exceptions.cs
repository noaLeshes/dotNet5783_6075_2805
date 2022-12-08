using System.Xml.Linq;

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
    public int EntityId;
    public string EntityName;
    public BlAlreadyExistsEntityException(string name, int id, string message)
        : base(message) { EntityName = name;  EntityId = id; }
    public BlAlreadyExistsEntityException(string name, int id, string message, Exception innerException)
        : base(message, innerException) { EntityName = name; EntityId = id; }
    public override string ToString()
    {
        if (EntityId != -1)
        {
            return $"{EntityName} number already exists in an order.";
        }
        else
        {
            return $"{EntityName} number {EntityId} already exists.";
        }
    }
}
[Serializable]
public class BlNullPropertyException : Exception
{
    public string PropertyName;
    public BlNullPropertyException(string name)
        : base() { PropertyName = name; }
    public BlNullPropertyException(string name, string massage)
        : base(massage) { PropertyName = name; }
    public BlNullPropertyException(string name, string massage, Exception innerException)
        : base(massage, innerException) { PropertyName = name; }
    public override string ToString()
    {
        return $"{PropertyName} is empty";
    }
        
}

[Serializable]
public class BlIncorrectDateException : Exception
{
    public string ex;
    
    public BlIncorrectDateException(string name)
        : base() { ex = name;  }
    public BlIncorrectDateException(string name, string massage)
        : base(massage) { ex = name; }
    public BlIncorrectDateException(string name, string massage, Exception innerException)
        : base(massage, innerException) { ex = name; }
    public override string ToString()
    {
            return $"{ ex}";
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
        return base.ToString();
    }
}
[Serializable]
public class BlInvalidExspressionException : Exception
{
    public string PropertyName;
    public BlInvalidExspressionException(string name)
        : base() { PropertyName = name; }
    public BlInvalidExspressionException(string name, string massage)
        : base(massage) { PropertyName = name; }
    public BlInvalidExspressionException(string name, string massage, Exception innerException)
        : base(massage, innerException) { PropertyName = name; }
    public override string ToString()
    {
        return $"The {PropertyName} is invalid";
    }

}
[Serializable]
public class BlNotInStockException : Exception
{
    public string ProductName;
    public int ProductId;
    public BlNotInStockException(string name, int id)
        : base() { ProductName = name; ProductId = id; }
    public BlNotInStockException(string name, int id, string massage)
        : base(massage) { ProductName = name; ProductId = id; }
    public BlNotInStockException(string name, int id, string massage, Exception innerException)
        : base(massage, innerException) { ProductName = name; ProductId = id; }
    public override string ToString()
    {
            return $"Not enough of {ProductName} in stock";
    }
}
[Serializable]
public class BlWrongChoiceException : Exception
{
    public BlWrongChoiceException()
        : base() { }
    public BlWrongChoiceException(string massage)
        : base(massage) { }
    public BlWrongChoiceException(string massage, Exception innerException)
        : base(massage, innerException) { }
    public override string ToString()
    {
        return $"Wrong choice";
    
    }
}

