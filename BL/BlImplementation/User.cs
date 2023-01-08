using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
namespace BlImplementation;

internal class User : IUser
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public void AddUser(BO.User u)
    {
        if(u.UserName == null || u.UserName == "")
        {
            throw new BO.BlNullPropertyException("User Name");
        }
        if (u.Password == null || u.Password == "")
        {
            throw new BO.BlNullPropertyException("Password");
        }
        try
        {
            dal?.User.Add(new DO.User()
            {
                UserName = u.UserName ?? "",
                Password = u.Password ?? "0",
                UserStatus = (DO.UserStatus) u?.UserStatus!,  
            });
        }
        catch(DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistsEntityException("User", 0);
        }

    }
    public BO.User GetByUserName(string userName  )
    {
        try
        {
            if (userName == null || userName == "")
            {
                throw new BO.BlNullPropertyException("User Name");
            }

            DO.User? u = dal?.User.GetByUser(userName);
            return new BO.User()
            {
                UserName = u?.UserName ?? "",
                UserStatus = (BO.UserStatus)u?.UserStatus!,
                Password = u?.Password ?? ""
            };
        }
        catch (DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistsEntityException("User", 0);
        }
       
    }
    
}
