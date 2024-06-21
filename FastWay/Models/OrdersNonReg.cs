namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdersNonReg")]
    public partial class OrdersNonReg
    {
        public int ID { get; set; }

        public int? OrderID { get; set; }

    }
}