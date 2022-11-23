
using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product product)
    {
        bool isExist = DataSource.ProductList.Contains(product);
        if (isExist)
            throw new Exception("not found");
        DataSource.ProductList.Add(product);
        return product.ID;
    }
    public Product GetById(int id)
    {
        Product p = DataSource.ProductList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        return p;
    }
    public void Update(Product p)
    {
        bool isExist = DataSource.ProductList.Contains(p);
        if (isExist)
        {
            Product? pr = DataSource.ProductList.Find(x => x?.ID == p.ID);
            DataSource.ProductList.Remove(pr);
            DataSource.ProductList.Add(p);//updating the wanted order
            return;
        }
        throw new Exception("no product found");
    }
    public void Delete(int id)
    {
        Product p = DataSource.ProductList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        DataSource.ProductList.Remove(p);
    }
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> newList = new List<Product?>();
        foreach (Product o in DataSource.ProductList)
        {
            newList.Add(o);
        }
        return newList;
    }
}
