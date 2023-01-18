using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal;

internal class DalUser : IUser
{
    readonly string s_users = "users";
    static DO.User? createStudentfromXElement(XElement u)
    {
        return new DO.User()
        {
            Password = (string?)u?.Element("Password")!,
            Name = (string?)u?.Element("Name"),
            UserStatus = (string?)u?.Element("UserStatus") == "MANAGER" ? UserStatus.MANAGER : UserStatus.CUSTOMER,
            UserGmail = (string?)u?.Element("UserGmail")!,
            Address = (string?)u?.Element("Address")
        };
    }
    public void Add(User u)
    {
        XElement rootUser =  XMLTools.LoadListFromXMLElement(s_users);
        XElement? user;
            user = (from item in rootUser.Elements()
                    where item?.Element("Password")!.Value == u.Password && item?.Element("UserGmail")!.Value == u.UserGmail
                    select item).FirstOrDefault();
                   
        if(user == null)
        {
            throw new DalAlreadyExistsIdException(0, "User");
        }
        XElement userElem = new XElement("User",
                                  new XElement("Name", u.Name),
                                  new XElement("Email", u.UserGmail),
                                  new XElement("UserStatus", u.UserStatus),
                                  new XElement("Password", u.Password),
                                  new XElement("Address", u.Address));
        rootUser.Add(userElem);
        XMLTools.SaveListToXMLElement(rootUser, s_users);
    }

    public User GetByUser(string userName, string password)
    {
        XElement rootUser = XMLTools.LoadListFromXMLElement(s_users);

        return (from u in rootUser?.Elements()
                where u?.Element("Password")!.Value == password && u?.Element("UserGmail")!.Value == userName
                select (DO.User?)createStudentfromXElement(u)).FirstOrDefault()
                ?? throw new DalMissingIdException(0, "User");//looking if the user exists and returning its details
    }
}
