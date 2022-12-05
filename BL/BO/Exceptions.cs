﻿namespace DO;

public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? message) : base(message) { }
}
public class DalDataCorruptionException : Exception
{
    public DalDataCorruptionException(string? message) : base(message) { }
}
