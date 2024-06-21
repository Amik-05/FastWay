namespace FastWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        public int Id { get; set; }

        [Column("login")]
        [StringLength(100)]
        public string login1 { get; set; }

        [StringLength(100)]
        public string password { get; set; }
    }
}
