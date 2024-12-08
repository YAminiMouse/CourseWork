namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeRoom")]
    public partial class TypeRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeRoom()
        {
            Room = new HashSet<Room>();
        }

        public int Id { get; set; }

        public int IdComfort { get; set; }

        public int IdSize { get; set; }

        public int cost { get; set; }

        public string description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deleteDate { get; set; }

        public byte[] photo { get; set; }

        public virtual Capacity Capacity { get; set; }

        public virtual Comfort Comfort { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Room { get; set; }
    }
}
