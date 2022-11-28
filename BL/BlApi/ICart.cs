using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
   Cart AddItem(Cart c, int productId);
    Cart UpdateItem(Cart c, int productId, int amount);
    void ConfirmCart(Cart c, string name, string address, string email);
}
