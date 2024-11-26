using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.AdditionalEntities
{
    public class Report
    {
        private List<Room> allRooms;
        public List<Room> AllRooms
        {
            get
            {
                return allRooms;
            }
        }
        private List<Room> busyRooms;
        public List<Room> BusyRooms
        {
            get
            {
                return busyRooms;
            }
        }
        private List<UserBookingExtension> bookings;
        public List<UserBookingExtension> Bookings
        {
            get
            {
                return bookings;
            }
        }
        private List<UserBookingExtension> busyRoomsBooking;
        public List<UserBookingExtension> BusyRoomsBooking
        {
            get
            {
                return busyRoomsBooking;
            }
        }
        private double totalSumStringService;
        public double TotalSumStringService
        {
            get
            {
                return totalSumStringService;
            }
        }
        private double totalSumBooking;
        public double TotalSumBooking
        {
            get
            {
                return totalSumBooking;
            }
        }
        public Report(List<Room> allRooms , List<Room> busyRooms , List<UserBookingExtension> bookings , List<UserBookingExtension> busyRoomsBooking , double totalSumStringService, double totalSumBooking) 
        {
            this.allRooms = allRooms;
            this.busyRooms = busyRooms;
            this.bookings = bookings;
            this.busyRoomsBooking = busyRoomsBooking;
            this.totalSumStringService = totalSumStringService;
            this.totalSumBooking = totalSumBooking;

        }
    }
}
