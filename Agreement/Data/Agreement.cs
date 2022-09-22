using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agreement.Data
{
    public class Agreement //: IdentityUser
    {
        public int AgreementId { get; set; }
        //[PersonalData]
        //public string ? Name { get; set; }
        public string ? UserId { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public Product ? Product { get; set; }
        public int ProductGroupId { get; set; }
        [DisplayName("Product Group")]
        [Required]
        public ProductGroup ? ProductGroup { get; set; }
        [DisplayName("Effective Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("Expiration Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime ExpirationDate { get; set; }
        [DisplayName("Product Price")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value Must Bigger Than {1}")]
        [DisplayName("New Price")]
        public decimal NewPrice { get; set; }
        [Required]
        public bool Active { get; set; }
    }
    
}
