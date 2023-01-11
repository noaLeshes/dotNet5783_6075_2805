using DalApi;
using DO;
namespace Dal;

internal class DalUser : IUser
{
    public void Add(User u)
    {
        var u1 = DataSource.UserList.Find(x => x?.Password == u.Password && x?.UserGmail == u.UserGmail);
        if (u1 != null)
            throw new DalAlreadyExistsIdException(0, "User");
      
        DataSource.UserList.Add(u);

    }
    public User GetByUser(string userMail, string password)
    {
        return DataSource.UserList.Find(x => x?.UserGmail == userMail && x?.UserGmail == password) ?? throw new DalMissingIdException(0, "User");
    }
}
   

