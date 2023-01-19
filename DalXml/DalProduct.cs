using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string s_products = "products";

    public int Add(Product item)
    {

        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (p.FirstOrDefault(x => x?.ID == item.ID)!=null)
        {
            throw new DalAlreadyExistsIdException(item.ID, "Product");
        }
        item.ID = Config.GetNextProductId();
        p.Add(item);
        Config.SetNextProductId(item.ID+1);
        XMLTools.SaveListToXMLSerializer(p, s_products);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (p.RemoveAll(x => x?.ID == id) == 0)
        {
            throw new DalMissingIdException(id, "Product");
        };
        XMLTools.SaveListToXMLSerializer(p, s_products);
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if(filter==null)
        {
            return p.Select(x => x).OrderBy(x => x?.ID);
        }
        else
        {
            return p.Where(filter).OrderBy(x => x?.ID);
        }
    }

    public Product GetByFilter(Func<Product?, bool>? filter = null)
    {
        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        return p.Find(x=>filter!(x)) ?? throw new DalMissingIdException(-1, "Product");
    }

    public Product GetById(int id)
    {
        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        return p.FirstOrDefault(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Product");
    }

    public void Update(Product item)
    {
        Delete(item.ID);
        List<DO.Product?> p = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (p.FirstOrDefault(x => x?.ID == item.ID) != null)
        {
            throw new DalAlreadyExistsIdException(item.ID, "Product");
        }
        p.Add(item);
        XMLTools.SaveListToXMLSerializer(p, s_products);
    }
}
