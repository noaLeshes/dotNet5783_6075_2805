using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        if (DataSource.arrProductIndex == 50)
            throw new Exception("The array is full");//if the array is full throw
        for (int i = 0; i<DataSource.arrProductIndex; i++)
        {
            if (DataSource.productArr[i].ID == product.ID)   //if already exsist 
                throw new Exception("The product is already in stock");
        }
        DataSource.productArr[DataSource.arrProductIndex++] = product;
        return product.ID;
      
    }
    public Product GetById(int id)
    {
        Product p;
        for(int i=0; i<DataSource.arrProductIndex; i++)
        {
            p = DataSource.productArr[i];
            if (p.ID == id)//serch the array to find the wanted id
                return p;
        }
        throw new Exception("no products found");
    }
    public void Update(Product p)
    {
        for(int i = 0; i<DataSource.arrProductIndex; i++)
        {
            if (DataSource.productArr[i].ID == p.ID)
            {
                DataSource.productArr[i] = p;//updating the wanted product
                return;
            }     
        }
        throw new Exception("no product found");
    }
    public void Delete(int id)
    {
        for(int i = 0; i<DataSource.arrProductIndex; i++)
        {
            if (DataSource.productArr[i].ID == id)
            {
                DataSource.productArr[i] = DataSource.productArr[DataSource.arrProductIndex];
                //taking the last product and replacing it with what we want to delete
                DataSource.arrProductIndex--;//reducing the index to delete product
                return;
            }

        }
        throw new Exception("not found");//if not found
    }
    public Product[] GetAll()
    {
        Product[] newArr = new Product[DataSource.arrProductIndex];
        for (int i = 0; i < DataSource.arrProductIndex; i++)
        {
            newArr[i] = DataSource.productArr[i];//copy all the orders to the new array
        }
        return newArr;
    }
}
