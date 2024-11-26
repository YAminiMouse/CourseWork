using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.MainModel
{
    public class AdminReportsModel
    {
        public AdminReportsModel() 
        { 

        }

        public Report GetData(DateTime start , DateTime end)
        {
            Report report = null;
            DateTime truncatedToDaysStartDate = new DateTime(start.Year, start.Month, start.Day);
            DateTime truncatedToDaysEndDate = new DateTime(end.Year, end.Month, end.Day);

            using(HotelModel hm = new HotelModel())
            {
                var allRooms = (from room in hm.Room where room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null) select room).ToList();

                var busyRooms = (from room in hm.Room
                                 join booking in hm.Booking on room.Id equals booking.IdRoom
                                 where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                                 (booking.IdStatus == 2) &&
                                 (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                                 select room).ToList();

                var bookings = (from room in hm.Room
                                join booking in hm.Booking on room.Id equals booking.IdRoom
                                where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                                (booking.IdStatus == 1) &&
                                (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                                select booking).ToList();

                var busyRoomsBooking = (from booking in hm.Booking
                                        join room in hm.Room on booking.IdRoom equals room.Id
                                        where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                                        (booking.IdStatus == 2) &&
                                        (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                                        select booking).ToList();
                double totalSumStringService = 0.0;
                double totalSumBooking = 0.0;
                List<UserBookingExtension> busyRoomsBookingList = new List<UserBookingExtension>();
                foreach (var item in busyRoomsBooking)
                {
                    busyRoomsBookingList.Add(new UserBookingExtension(item));
                    totalSumBooking += item.Room.TypeRoom.cost * (end - start).Days;
                    var stringServiceList = (from str in hm.StringService where str.IdBooking == item.Id select str).ToList();
                    foreach (var stringService in stringServiceList)
                    {
                        totalSumStringService += stringService.cost;
                    }
                }
                List<UserBookingExtension> bookingsList = new List<UserBookingExtension>();
                foreach (var item in bookings)
                {
                    bookingsList.Add(new UserBookingExtension(item));
                }

                report = new Report(allRooms , busyRooms , bookingsList, busyRoomsBookingList, totalSumStringService , totalSumBooking);

            }

            return report;
        }
    }
}
