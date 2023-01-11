using BO;
namespace BlApi;

public interface IUser
{
    public void AddUser(User user, int position);
    public User GetByUserName(string userName, string password);
}
