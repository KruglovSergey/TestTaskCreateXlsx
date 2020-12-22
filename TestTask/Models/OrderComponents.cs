namespace TestTask.Models
{
    public partial class OrderComponents
    {
        public int? Id { get; set; }

        public int ProductCode { get; set; }

        public int? Amount { get; set; }

        public int OrderId { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual ProductLibrary ProductLibrary { get; set; }
    }
}