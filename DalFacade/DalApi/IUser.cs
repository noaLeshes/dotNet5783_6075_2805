namespace DalApi;
using DO;

public interface IUser 
{
    public void Add(User u);
    public User GetByUser(string userName);
}
