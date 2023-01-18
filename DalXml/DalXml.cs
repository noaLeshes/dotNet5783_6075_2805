using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml() { }
        public static IDal Instance { get; } = new DalXml();
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProduct();
        public IOrderItem OrderItem => new DalOrderItem();
        public IUser User => new DalUser();
    }
}
