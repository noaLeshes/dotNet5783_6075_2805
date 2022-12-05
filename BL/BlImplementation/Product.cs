using BlApi;
using BO;
using System.Diagnostics;
using System.Xml.Linq;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    public void AddProduct(int id, string name, double price, int amount)
    {
        int i = dal.Product.Add(new DO.Product()
        {
            ID = id > 0 ? id : throw new NullReferenceException("Missing id"),
            Name = name != "" ? name : throw new NullReferenceException("Missing name"),
            //Category = (DO.Category)c,
            Price = price > 0 ? price : throw new NullReferenceException("Wrong price"),
            InStock = amount > 0 ? amount : throw new NullReferenceException("Wrong amount")
        }) ;
    }

    public void DeleteProduct(int id)
    {
        foreach (DO.Order o in dal.Order.GetAll())
        {
            var list = from DO.Product p in dal.OrderItem.GetAllOrderProducts(o.ID)
                       where p.ID == id
                       select p;
            if (list != null)
                dal.Product.Delete(id);
        }
    }

    public IEnumerable<ProductItem?> GetProducts()
    {
        var list = dal.Product.GetAll();
        var productItems = new List<ProductItem?>();
        foreach (DO.Product p in list)
        {
            productItems.Add(new BO.ProductItem()
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Category)p.Category,
                InStock = p.InStock>0 ? true : false,
                Amount = p.InStock,
            });
        }
        return productItems;  
    }

   public IEnumerable<ProductForList?> GetProductsList()
    {
        return from DO.Product? p in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   ID = p?.ID ?? throw new NullReferenceException("Missing id"),
                   Name = p?.Name ?? throw new NullReferenceException("Missing name"),
                   Category = (BO.Category?)p?.Category ?? throw new NullReferenceException("Wrong category"),
                   Price = p?.Price ?? 0
               };
    }

   public BO.ProductItem GetProductDitailes(int id, BO.Cart c)
    {
        DO.Product p = dal.Product.GetById(id);
       return new BO.ProductItem()
        {
            Id = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (BO.Category)p.Category,
            InStock = p.InStock > 0 ? true : false,
            Amount = p.InStock,
        };
    }

    public BO.Product GetProductDitailesManager(int id)
    {
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

    public void UpdateProduct(BO.Product p)
    {
         dal.Product.Update(new DO.Product()
        {
             ID = p.Id > 0 ? p.Id : throw new NullReferenceException("Missing id"),
             Name = p.Name != "" ? p.Name : throw new NullReferenceException("Missing name"),
             Category = (DO.Category)p.Category,
             Price = p.Price > 0 ? p.Price : throw new NullReferenceException("Wrong price"),
             InStock = p.InStock > 0 ? p.InStock : throw new NullReferenceException("Wrong amount")
         });
    }
}
