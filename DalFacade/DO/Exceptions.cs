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
                return $"{EntityName} number {EntityID} does not exist.";
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
