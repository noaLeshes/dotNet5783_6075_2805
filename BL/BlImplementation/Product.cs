using BlApi;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{
    BO.Cart IProduct.AddProduct(int id, string name, double price, int amount)
    {
        throw new NotImplementedException();
    }

    BO.Cart IProduct.DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<ProductItem?> IProduct.GetProducts()
    {
        throw new NotImplementedException();
    }

    IEnumerable<ProductForList?> IProduct.GetProductsList()
    {
        throw new NotImplementedException();
    }

    BO.Product IProduct.GetProsuctDitailes(int id, BO.Cart c)
    {
        throw new NotImplementedException();
    }

    BO.Product IProduct.GetProsuctDitailesManager(int id)
    {
        throw new NotImplementedException();
    }

    BO.Cart IProduct.UpdateProduct(int id, string name, double price, int amount)
    {
        throw new NotImplementedException();
    }
}
