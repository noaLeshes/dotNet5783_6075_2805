using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string CostomerName { get; set; }
    public string CostomerEmail { get; set; }
    public string CostomerAddress { get; set; }  
    public OrderItem Items { get; set; } 
    public double TotalPrice { get; set; }  
}
