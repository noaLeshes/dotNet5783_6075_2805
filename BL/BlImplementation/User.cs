﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DO;
using BO;
namespace BlImplementation;

internal class User : IUser
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public void AddUser(BO.User u, int position)
    {
        if (u.UserGmail == null || u.UserGmail == "")//if one of the details is empty
        {
            throw new BO.BlNullPropertyException("Email");
        }
        if (u.Password == null || u.Password == "")
        {
            throw new BO.BlNullPropertyException("Password");
        }
        if (u.Name == null || u.Name == "")
        {
            throw new BO.BlNullPropertyException("Name");
        }
        if (u.Address == null || u.Address == "")
        {
            throw new BO.BlNullPropertyException("Address");
        }
        if (!u.UserGmail.Contains("@"))//if the email is invalid
        {
            throw new BO.BlInvalidExspressionException("Email");
        }
        try
        {
            dal?.User.Add(new DO.User()//adding the user with th given details 
            {
                Name = u.Name ?? "",
                Password = u.Password ?? "0",
                UserStatus = (DO.UserStatus)position,
                UserGmail=u.UserGmail  ,
                Address=u.Address


            });
        }
        catch(DalAlreadyExistsIdException ex)//if the user already exists 
        {
            throw new BO.BlAlreadyExistsEntityException("User", 0);
        }

    }
    public BO.User GetByUserName(string name, string password )
    {
        try
        {
            if (name == null || name == "")//if one of the details is empty
            {
                throw new BO.BlNullPropertyException("Email");
            }
            if (password == null || password == "")
            {
                throw new BO.BlNullPropertyException("Password");
            }

            DO.User? u = dal?.User.GetByUser(name, password);//getting the user's details
            return new BO.User()
            {
                Name = u?.Name ?? "",
                Password = u?.Password ?? "",
                UserStatus = (BO.UserStatus)u?.UserStatus!,
                UserGmail = u?.UserGmail,
                Address = u?.Address
            };
        }
        catch (DO.DalMissingIdException ex)//if the user doesn't exist
        {
            throw new BO.BlMissingEntityException(@"User doesn't exist,
                                        Sign up first",ex);
        }
       
    }
    
}
