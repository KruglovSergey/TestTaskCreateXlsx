using System.Collections.Generic;

namespace TestTask.Models
{
    public partial class ProductLibrary
    {
        public ProductLibrary()
        {
            OrderComponents = new HashSet<OrderComponents>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public string Price { get; set; }

        public virtual ICollection<OrderComponents> OrderComponents { get; set; }
    }
}