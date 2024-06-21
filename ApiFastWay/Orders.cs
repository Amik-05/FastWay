namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            AcceptOrder = new HashSet<AcceptOrder>();
        }

        public int ID { get; set; }

        [StringLength(1000)]
        public string FIO { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public int? DeliveryType { get; set; }

        public int? CargoID { get; set; }

        public int? IsAccepted { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        public DateTime DateOrder { get; set; }

        [StringLength(15)]
        public string trackNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcceptOrder> AcceptOrder { get; set; }

        public virtual AcceptOrder AcceptOrder1 { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual DeliveryType DeliveryType1 { get; set; }
    }
}
