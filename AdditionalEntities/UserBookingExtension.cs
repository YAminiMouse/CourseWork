using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class UserBookingExtension
    {
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepatureDate { get; set; }
        public double FinalCost { get; set; }   
        public string StatusName { get; set; }
        public int IdRoom { get; set; }
        public int IdUser { get; set; }
        public int RoomNumber { get; set; }
        public string TypeRoom { get; set; }
        public string UserName { get; set; }

        public UserBookingExtension(Booking booking)
        {
            Id = booking.Id;
            ArrivalDate = booking.ArrivalDate;
            DepatureDate = booking.DepatureDate;
            FinalCost = booking.FinalCost;
            StatusName = booking.Status.name;
            IdUser = booking.IdUser;
            IdRoom = booking.IdRoom;
            RoomNumber = booking.Room.number;
            TypeRoom = booking.Room.TypeRoom.name;
            UserName = booking.User.FIO;
        }
    }
}