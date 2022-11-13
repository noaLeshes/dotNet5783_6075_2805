using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        if (DataSource.Config.arrProductIndex == 50)
            throw new Exception("the array is full");
        DataSource.ProductArr[DataSource.Config.arrProductIndex] = product;
        return product.ID;
       //להוסיף בדיקה עםהפריט כבר קיים או לא
    }
    public Product GetById(int id)
    {
        Product p;
        for(int i=0; i<50; i++)
        {
            p = DataSource.ProductArr[i];
            if (p.ID == id)
                return p;
        }
        throw new Exception("no products found");
    }
    public void Update(Product p)
    {
        Product d;
    }
    public void Delete(int id)
    {

    }
    public IEnumerable<Order?> GetAll()
    {
        
    }
}
