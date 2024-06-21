namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drivers")]
    public partial class Drivers
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        [NotMapped]
        public string FIO { get; set; }
        public string Age { get; set; }
        public string Status { get; set; }
        public int? CarID { get; set; }
        
    }
}
