namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }

        [Required]
        public string FIO { get; set; }

        public string number { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        public string password { get; set; }

        public double? moneySpent { get; set; }

        public int? IdDiscount { get; set; }

        public int IdRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Role Role { get; set; }
    }
}
