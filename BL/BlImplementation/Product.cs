using BlApi;
using BO;
using System.Collections.Generic;
using System.Xml.Linq;


namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    public void AddProduct(int id, string name, double price, int amount, BO.Category c)
    {
        try
        {
            int i = dal.Product.Add(new DO.Product() // adding a product with the wanted details
            {
                ID = id > 0 ? id : throw new BO.BlInvalidExspressionException("Id"),// throwing if the detail is invalid
                Name = name != "" ? name : throw new BO.BlNullPropertyException("Name"),
                Category = (DO.Category)c ,
                Price = price > 0 ? price : throw new BO.BlInvalidExspressionException("Price"),
                InStock = amount > 0 ? amount : throw new BO.BlInvalidExspressionException("Amount in stock")
            });
        }
        catch(DO.DalAlreadyExistsIdException )// if the product already exists
        {
            throw new BO.BlAlreadyExistsEntityException("Product", id);
        }
    }
    
    public void DeleteProduct(int id)
    {
        try
        {
            if(id < 0)// if id is invalid
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            var list = dal.OrderItem.GetAll();// list of all orderItems
            var listtodel = list.Where(x => x?.ProductId == id);// finding orderItems with the same id as id
            if (listtodel.Any() )// if aproduct with id found in an order
                throw new BO.BlAlreadyExistsEntityException("Product", -1);// throw
            dal.Product.Delete(id);// if not found 
        }
        catch(DO.DalMissingIdException exception)// if product doesn't exist
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public IEnumerable<ProductForList?> GetProductsListByCategory(BO.Category c)
    {
        return from DO.Product? p in dal.Product.GetAll(p => (BO.Category)p.Value.Category == c)// getting all the products
               select new BO.ProductForList// conversion from product to productForList
               {
                   ID = p?.ID ?? throw new BO.BlInvalidExspressionException("Id"),// throwing if the detail is invalid
                   Name = p?.Name ?? throw new BO.BlNullPropertyException("Name"),
                   Category = (BO.Category?)p?.Category ?? throw new BO.BlWrongCategoryException(),// throwing if wrong category
                   Price = p?.Price ?? 0
               };
    }

   public IEnumerable<ProductForList?> GetProductsList()
    {
        return from DO.Product? p in dal.Product.GetAll()// getting all the products
               select new BO.ProductForList// conversion from product to productForList
               {
                   ID = p?.ID ?? throw new BO.BlInvalidExspressionException("Id"),// throwing if the detail is invalid
                   Name = p?.Name ?? throw new BO.BlNullPropertyException("Name"),
                   Category = (BO.Category?)p?.Category ?? throw new BO.BlWrongCategoryException(),// throwing if wrong category
                   Price = p?.Price ?? 0 
               };
    }

   public BO.ProductItem GetProductDitailes(int id, BO.Cart c)
    {
        try
        {
            if (id < 0)// if id is invalid
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Product p = dal.Product.GetById(id);
            return new BO.ProductItem()// conversion from product to productItem
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Category)p.Category,
                InStock = p.InStock > 0 ? true : false,
                Amount = c.Items == null? 0: (from i in c.Items// calculating the amount
                         where i.ProductId == id
                         select i.Amount).Sum()
            };
        }
        catch (DO.DalMissingIdException exception)// if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public BO.Product GetProductDitailesManager(int id)
    {
        try
        {
            if (id < 0)// if id is invalid
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Product p = dal.Product.GetById(id);
            return new BO.Product()// conversion from doProduct to boProduct
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Category)p.Category,
                InStock = p.InStock
            };
        }
        catch (DO.DalMissingIdException exception)// if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public void UpdateProduct(BO.Product p)
    {
        try
        {
            dal.Product.Update(new DO.Product()// updating the product
            {
                ID = p.Id > 0 ? p.Id : throw new BO.BlInvalidExspressionException("Id"),// throwing if the detail is invalid
                Name = p.Name != "" ? p.Name : throw new BO.BlNullPropertyException("Name"),
                Category = (DO.Category)p.Category,
                Price = p.Price > 0 ? p.Price : throw new BO.BlInvalidExspressionException("Price"),
                InStock = p.InStock > 0 ? p.InStock : throw new BO.BlInvalidExspressionException("Amount in stock")//if there are not enough products in stock
            });
        }
        catch (DO.DalMissingIdException exception)// if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }
}
