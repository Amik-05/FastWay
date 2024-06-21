namespace FastWayForAdministrator.Models
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

        [StringLength(100)]
        public string Email { get; set; }

        public int? DeliveryType { get; set; }

        public int? CargoID { get; set; }

        public int? IsAccepted { get; set; }

        public string Phone { get; set; }

        public DateTime? DateOrder { get; set; }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }
        [NotMapped]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcceptOrder> AcceptOrder { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual DeliveryType DeliveryType1 { get; set; }
    }
}
