using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models
{
    public partial class Orders
    {
        public int Id { get; set; }

        public int ShopsId { get; set; }

        public bool? OrderAgreed { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateOrderAgreed { get; set; }

        public string DraftOrder { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateDraftOrder { get; set; }

        public virtual OrderComponents OrderComponents { get; set; }

        public virtual Shops Shops { get; set; }
    }
}