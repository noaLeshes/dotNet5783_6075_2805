using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        if (DataSource.Config.arrProductIndex == 50)
            throw new Exception("The array is full");
        for(int i = 0; i<DataSource.Config.arrProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == product.ID)    
                throw new Exception("The product is already in stock");
        }
        DataSource.ProductArr[DataSource.Config.arrProductIndex] = product;
        return product.ID;
      
    }
    public Product GetById(int id)
    {
        Product p;
        for(int i=0; i<DataSource.Config.arrProductIndex; i++)
        {
            p = DataSource.ProductArr[i];
            if (p.ID == id)
                return p;
        }
        throw new Exception("no products found");
    }
    public void Update(Product p)
    {
        for(int i = 0; i<DataSource.Config.arrProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == p.ID) 
                DataSource.ProductArr[i] = p;
            else
                throw new Exception("no products found");
        }
    }
    public void Delete(int id)
    {
        for(int i = 0; i<DataSource.Config.arrProductIndex; i++)
        {
            if (DataSource.ProductArr[i].ID == id)
            {
                DataSource.ProductArr[i] = DataSource.ProductArr[DataSource.Config.arrProductIndex];
                //DataSource.ProductArr[DataSource.Config.arrProductIndex] = 0;
                DataSource.Config.arrProductIndex--;
            }

        }
    }
    public Product[] GetAll()
    {
        Product[] newArr = new Product[DataSource.Config.arrProductIndex];
        for (int i = 0; i < DataSource.Config.arrProductIndex; i++)
        {
            newArr[i] = DataSource.ProductArr[i];
        }
        return newArr;
    }
}
