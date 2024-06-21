namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Orders")]
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            AcceptOrder = new HashSet<AcceptOrder>();
        }

        public int ID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        public int? DeliveryType { get; set; }

        public int? CargoID { get; set; }

        public int? IsAccepted { get; set; }

        
        public string Phone { get; set; }

        public DateTime DateOrder { get; set; }
        public DateTime SendingDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string SummaryCost { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
        [NotMapped]
        public string Status { get; set; }
        public string IsNeedMovers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcceptOrder> AcceptOrder { get; set; }

        public virtual AcceptOrder AcceptOrder1 { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual DeliveryType DeliveryType1 { get; set; }
    }
}
