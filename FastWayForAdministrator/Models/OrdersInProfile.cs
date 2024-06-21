namespace FastWayForAdministrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdersInProfile")]
    public partial class OrdersInProfile
    {
        public int ID { get; set; }

        public int? OrderID { get; set; }

        public int? ProfileID { get; set; }
    }
}
