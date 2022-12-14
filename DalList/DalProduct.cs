using DalApi;
using DO;
namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product product)
    { 
       var p = DataSource.ProductList.Find(x => x?.ID == product.ID);
       if(p != null)   
            throw new DalAlreadyExistsIdException(product.ID, "Product");
        DataSource.ProductList.Add(product);
        return product.ID;
    }
    public Product GetById(int id)
    {
        return DataSource.ProductList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Product");
    }
    public void Update(Product p)
    {
        //var isExist = DataSource.OrderList.Find(x => x?.ID == p.ID);
        var isExist = DataSource.ProductList.Find(x => x?.ID == p.ID);
        if (isExist != null)
        {
            Product? pr = DataSource.ProductList.Find(x => x?.ID == p.ID);
            DataSource.ProductList.Remove(pr);
            DataSource.ProductList.Add(p);//updating the wanted order
            return;
        }
        else
            throw new DalMissingIdException(p.ID, "Product");
    }
    public void Delete(int id)
    {
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Product"));
    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if(filter == null)
            return new List<Product?>(DataSource.ProductList);
        else
            return new List<Product?>(DataSource.ProductList).Where(p => filter(p));
    }
    public Product GetByFilter(Func<Product?, bool>? filter = null)
    {
        return DataSource.ProductList.Find(x => filter!(x)) ?? throw new DalMissingIdException(-1, "Product");
    }
}
