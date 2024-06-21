namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cargo")]
    public partial class Cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cargo()
        {
            Orders = new HashSet<Orders>();
        }

        public int ID { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        public int? Category { get; set; }

        public int? Subcategory { get; set; }

        public decimal? OverallVolume { get; set; }

        public decimal? TotalWeight { get; set; }

        public virtual Category Category1 { get; set; }

        public virtual Subcategory Subcategory1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
