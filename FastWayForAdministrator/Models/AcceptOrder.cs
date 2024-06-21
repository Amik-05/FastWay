namespace FastWayForAdministrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcceptOrder")]
    public partial class AcceptOrder
    {
        public int ID { get; set; }

        public int? AcceptOrderInfo { get; set; }

        [StringLength(3)]
        public string Accept { get; set; }

        [StringLength(1000)]
        public string reasonForRejection { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
