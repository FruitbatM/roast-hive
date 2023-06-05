using System.ComponentModel.DataAnnotations;

namespace RoastHiveMvc.Models;
public class Product
{
    [Key]
    public int ProdID { get; set; }
    public string? CatId { get; set; }
    [Required] //Made required for validation purposes
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string? Size { get; set; }

    public double UnitPrice { get; set; }

    public string? Origin { get; set; }
    public string? Url { get; set; }

}