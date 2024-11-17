namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            StringService = new HashSet<StringService>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ArrivalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DepatureDate { get; set; }

        public double FinalCost { get; set; }

        public int IdStatus { get; set; }

        public int IdRoom { get; set; }

        public int IdUser { get; set; }

        public virtual User User { get; set; }

        public virtual Room Room { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StringService> StringService { get; set; }
    }
}
