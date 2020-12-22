using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models
{
    public partial class Shops
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Director { get; set; }

        public virtual ICollection<Orders> Order { get; set; }
    }
}