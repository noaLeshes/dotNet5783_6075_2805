using DalApi;
using DO;
namespace Dal;

internal class DalUser : IUser
{
    public void Add(User u)//adding a new user
    {
        var u1 = DataSource.UserList.Find(x => x?.Password == u.Password && x?.UserGmail == u.UserGmail);//looking if the user exists
        if (u1 != null)//if it was found
            throw new DalAlreadyExistsIdException(0, "User");
        DataSource.UserList.Add(u);//if it wasn't found

    }
    public User GetByUser(string userGmail, string password)//getting the user's details 
    {
        return DataSource.UserList.Find(x => (x?.UserGmail == userGmail && x?.Password == password)) ?? throw new DalMissingIdException(0, "User");//looking if the user exists and returning its details

    }
}
   

