using Microsoft.Build.Framework;
using System.ComponentModel;

namespace Agreement.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        [DisplayName("Product Description")]
        [Required]
        public string ProductDescription { get; set; }
        [DisplayName("Product Number")]
        [Required]
        public string ProductNumber { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int ProductGroupId { get; set; }
        [DisplayName("Product Group")]
        public ProductGroup ? ProductGroup { get; set; }

    }
}
