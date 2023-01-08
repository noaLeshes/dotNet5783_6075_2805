using DalApi;
using DO;
namespace Dal;

internal class DalUser : IUser
{
    public void Add(User u)
    {
        //var u1 = DataSource.UserList.Find(x => x?.Password == u.Password);
        //if (u1 != null)
        //    throw new DalAlreadyExistsIdException(0, "Password");
        var u2 = DataSource.UserList.Find(x => x?.UserName == u.UserName);
        if (u2 != null)
            throw new DalAlreadyExistsIdException(0, "User Name");
        DataSource.UserList.Add(u);

    }
    public User GetByUser(string userName)
    {
        return DataSource.UserList.Find(x => x?.UserName == userName) ?? throw new DalMissingIdException(0, "User");
    }
}
    //[Obsolete("can't use this functions", true)]
    //int Add(User u)
    //{
    //    return 0;
    //}
    //User GetById(int id)
    //{
    //    User u = new User();
    //    return u;
    //}
    //void Update(User u) { }
    //void Delete(int id) { }
    //IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
    //{
    //    if (filter == null)
    //        return new List<User?>(DataSource.UserList);
    //    else
    //        return new List<User?>(DataSource.UserList).Where(p => filter(p));
    //}
    //User GetByFilter(Func<User?, bool>? filter = null)
    //{
    //    return DataSource.UserList.Find(x => filter!(x)) ?? throw new DalMissingIdException(-1, "User");

    //}



