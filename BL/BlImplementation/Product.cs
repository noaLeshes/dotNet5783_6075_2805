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
           // DO.Product p = dal.Product.GetById(id);
            int i = dal.Product.Add(new DO.Product()
            {
                ID = id > 0 ? id : throw new BO.BlInvalidExspressionException("Id"),
                Name = name != "" ? name : throw new BO.BlNullPropertyException("Name"),
                Category = (DO.Category)c,
                Price = price > 0 ? price : throw new BO.BlInvalidExspressionException("Price"),
                InStock = amount > 0 ? amount : throw new BO.BlNotInStockException(name, id)            });
        }
        catch(DO.DalAlreadyExistsIdException exception)
        {
            throw new BO.BlAlreadyExistsEntityException("Product", id, exception);
        }
    }

    public void DeleteProduct(int id)
    {
        try
        {
            if(id < 0)
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            var list = dal.OrderItem.GetAll();
            var listtodel = list.Where(x => x?.ProductId == id);
            if (listtodel.Any() )
                throw new BO.BlAlreadyExistsEntityException("Product", -1);
            dal.Product.Delete(id);
        }
        catch(DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    //public IEnumerable<ProductItem?> GetProducts()
    //{
    //    try
    //    {
    //        var list = dal.Product.GetAll();
    //        var productItems = new List<ProductItem?>();
    //        foreach (DO.Product p in list)
    //        {
    //            productItems.Add(new BO.ProductItem()
    //            {
    //                Id = p.ID,
    //                Name = p.Name,
    //                Price = p.Price,
    //                Category = (BO.Category)p.Category,
    //                InStock = p.InStock > 0 ? true : false,
    //                Amount = p.InStock,
    //            });
    //        }
    //        return productItems;
    //    }
    //    catch (DO.DalAlreadyExistsIdException exception)
    //    {
    //        throw new BO.BlAlreadyExistsEntityException("Product alresdy exists", exception);
    //    }

    //}

   public IEnumerable<ProductForList?> GetProductsList()
    {
        return from DO.Product? p in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   ID = p?.ID ?? throw new BO.BlInvalidExspressionException("Id"),
                   Name = p?.Name ?? throw new BO.BlNullPropertyException("Name"),
                   Category = (BO.Category?)p?.Category ?? throw new BO.BlWrongCategoryException(),
                   Price = p?.Price ?? 0 
               };
    }

   public BO.ProductItem GetProductDitailes(int id, BO.Cart c)
    {
        try
        {
            if (id < 0)
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Product p = dal.Product.GetById(id);
            //IEnumerable<OrderItem?> cartItems = c.Items;
            //var a = cartItems.Where(x => x?.ProductId == id);
            //var a = from i in c.Items
            //       where i.Id == id
            //            select i.Amount;
            return new BO.ProductItem()
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Category)p.Category,
                InStock = p.InStock > 0 ? true : false,
                Amount = c.Items == null? 0: (from i in c.Items
                         where i.ProductId == id
                         select i.Amount).Sum() /*a.Sum(x =>x?.Amount??0)*/ 
            };
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public BO.Product GetProductDitailesManager(int id)
    {
        try
        {
            if (id < 0)
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Product p = dal.Product.GetById(id);
            return new BO.Product()
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Category)p.Category,
                InStock = p.InStock
            };
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public void UpdateProduct(BO.Product p)
    {
        try
        {
            dal.Product.Update(new DO.Product()
            {
                ID = p.Id > 100000 ? p.Id : throw new BO.BlInvalidExspressionException("Id"),
                Name = p.Name != "" ? p.Name : throw new BO.BlNullPropertyException("Name"),
                Category = (DO.Category)p.Category,
                Price = p.Price > 0 ? p.Price : throw new BO.BlInvalidExspressionException("Price"),
                InStock = p.InStock > 0 ? p.InStock : throw new BO.BlNotInStockException(p.Name, p.Id)
            });
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }
}
