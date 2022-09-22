using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agreement.Data
{
    public class ProductGroup
    {
        public int ProductGroupId { get; set; }
        [DisplayName("Group Description")]
        [Required]
        public string ? GroupDescription { get; set; }
        [DisplayName("Group Code")]
        [Required]
        public string ? GroupCode { get; set; }
        public bool Active { get; set; }

    }
}
