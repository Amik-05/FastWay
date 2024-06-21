namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MoversInOrder")]
    public partial class MoversInOrder
    {
       
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int Mover1 { get; set; }
        public int Mover2 { get; set; }
        
        
    }
}
