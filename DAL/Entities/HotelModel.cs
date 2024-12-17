using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class HotelModel : DbContext
    {
        public HotelModel()
            : base("name=HotelContext")
        {
        }

        public virtual DbSet<AddService> AddService { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Capacity> Capacity { get; set; }
        public virtual DbSet<Comfort> Comfort { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StringService> StringService { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeRoom> TypeRoom { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddService>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<AddService>()
                .HasMany(e => e.StringService)
                .WithRequired(e => e.AddService)
                .HasForeignKey(e => e.IdAddService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.StringService)
                .WithRequired(e => e.Booking)
                .HasForeignKey(e => e.IdBooking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Capacity>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Capacity>()
                .HasMany(e => e.TypeRoom)
                .WithRequired(e => e.Capacity)
                .HasForeignKey(e => e.IdSize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comfort>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Comfort>()
                .HasMany(e => e.TypeRoom)
                .WithRequired(e => e.Comfort)
                .HasForeignKey(e => e.IdComfort)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.User)
                .WithOptional(e => e.Discount)
                .HasForeignKey(e => e.IdDiscount);

            modelBuilder.Entity<Role>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Room)
                .HasForeignKey(e => e.IdRoom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.IdStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeRoom>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<TypeRoom>()
                .HasMany(e => e.Room)
                .WithRequired(e => e.TypeRoom)
                .HasForeignKey(e => e.IdTypeRoom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);
        }
    }
}
