namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cars")]
    public partial class Cars
    {
       
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Volume { get; set; }
        public string Max_weight { get; set; }
        public string VIN { get; set; }
        [NotMapped]
        public string NameAndVIN { get; set; }
        public string IsHaveDriver { get; set; }
        public string IsHaveDeliveryType { get; set; }
    }
}
