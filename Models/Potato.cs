namespace WebApplication1.Models
{
    public class Potato
    {
        public string SupplierName { get; set; }
        public double UnitWeight { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityAvailable { get; set; }
        public double PricePerPound { get { return UnitPrice / UnitWeight; } } 
    }

}
