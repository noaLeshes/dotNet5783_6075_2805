using BO;
namespace BlApi;

public interface IUser
{
    public void AddUser(User user, int position);//add user to the users list
    public User GetByUserName(string userName, string password);//get user by userName
}
