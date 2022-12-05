using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DO
{
    [Serializable]
    public class DalMissingIdException : Exception
    {
        public int EntityID;
        public string EntityName;

        public DalMissingIdException(int id, string name)
            : base() { EntityID = id; EntityName = name; }

        public DalMissingIdException(int id, string name, string massage)
            : base(massage) { EntityID = id; EntityName = name; }

        public DalMissingIdException(int id, string name, string massage, Exception innerException)
            : base(massage, innerException) { EntityID = id; EntityName = name; }

        public override string ToString()
        {
            if (EntityID != -1)
            {
                return $"{EntityName} number {EntityID} does not exist.";
            }
            else  //in case it's an orderItem and or the productID wasn't found or the orderID wasn't found 
            {
                return $"{EntityName} does not exist.";// לא נראה לי צריך סתם יותר מדי
            }
        }

    }
    [Serializable]
    public class DalAlreadyExistsIdException : Exception
    {
        public int EntityId;
        public string EntityName;

        public DalAlreadyExistsIdException(int id, string name) 
            : base() { EntityId = id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name, string message) 
            : base(message) { EntityId = id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name, string message, Exception inner)
                    : base(message, inner) { EntityId = id; EntityName = name; }
        public override string ToString()
        {
            return $"{EntityName} number {EntityId} already exists.";
        }
        
    }
}
