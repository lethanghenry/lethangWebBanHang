namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProduct")]
    public partial class UserProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProduct()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [StringLength(20)]
        public string idUser { get; set; }

        [StringLength(2000)]
        public string reviewUser { get; set; }

        [StringLength(20)]
        public string nameUser { get; set; }

        [StringLength(20)]
        public string emailUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
