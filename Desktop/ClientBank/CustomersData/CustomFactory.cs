using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersData
{
    static class CustomFactory
    {
        public static Customers GetCustomerFactory<T>(T model)
        {
            if (model is Busines) { var busines = model as Busines; return new Busines(busines); }
            else if (model is PhysicalClient) { var physical = model as PhysicalClient; return new PhysicalClient(physical); }
            else return null;
        }
    }
}
