namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StringService")]
    public partial class StringService
    {
        public int Id { get; set; }

        public int count { get; set; }

        public int cost { get; set; }

        public int IdAddService { get; set; }

        public int IdBooking { get; set; }

        public virtual AddService AddService { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
