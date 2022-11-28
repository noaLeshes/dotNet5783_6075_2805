using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum OrderStatus
{
    Initiated,
    Ordered,
    Paid,
    Shipped,
    Delivered,
}
public enum Category
{
    EYES, 
    FACE,
    BRUSHES, 
    LIPS, 
    BEAUTY
}
