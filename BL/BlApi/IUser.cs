using BO;
namespace BlApi;

public interface IUser
{
    public void AddUser(User user);
    public User GetByUserName(string userName);
}
