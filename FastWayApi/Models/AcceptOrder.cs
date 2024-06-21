namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcceptOrder")]
    public partial class AcceptOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcceptOrder()
        {
            Orders1 = new HashSet<Orders>();
        }

        public int ID { get; set; }

        public int? AcceptOrderInfo { get; set; }

        [StringLength(3)]
        public string Accept { get; set; }

        public virtual Orders Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders1 { get; set; }
    }
}
