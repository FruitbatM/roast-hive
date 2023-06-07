namespace RoastHiveMvc.Models;

public class CartItem
{
    public string? Name { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public string? Url { get; set; }
}