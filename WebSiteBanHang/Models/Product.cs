namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Product")]
    public partial class Product
    {
        [Key]
        [StringLength(20)]
        public string idProduct { get; set; }

        [StringLength(20)]
        public string nameProduct { get; set; }

        [StringLength(20)]
        public string pictureProduct { get; set; }

        public int? priceProduct { get; set; }

        public int? rateProduct { get; set; }

        public int? qualityProduct { get; set; }

        [StringLength(2000)]
        public string descriptionProduct { get; set; }

        [StringLength(20)]
        public string weightProduct { get; set; }

        [StringLength(20)]
        public string dismensionProduct { get; set; }

        [StringLength(20)]
        public string idCategory { get; set; }

        [StringLength(20)]
        public string idUser { get; set; }

        public virtual Category Category { get; set; }

        public virtual UserProduct UserProduct { get; set; }
    }
}
