using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_SalesDatabase.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
       // decimal in c# map to decimal in sql && double in c# map to float in sql 
      // decimal is ideal for financial and monetary calculations where precision is crucial
     // becuase double Represents numbers with floating-point approximation, which may lead to precision loss.
        public decimal Price { get; set; }

        public string Description { get; set; }
        public ICollection<Sale> Sales { get;  } = new List<Sale>();
    }
}
