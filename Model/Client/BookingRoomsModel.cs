using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class BookingRoomsModel
    {
        private Booking booking;
        public BookingRoomsModel() 
        { 
            booking = new Booking();
        }
        public List<TypeRoomExtension> GetTypes()
        {
            using (HotelModel hm = new HotelModel())
            {
                var types = (from typeroom in hm.TypeRoom select typeroom).ToList();
                List<TypeRoomExtension> typesExtensions = new List<TypeRoomExtension>();
                foreach (var type in types)
                {
                    typesExtensions.Add(new TypeRoomExtension(type, type.Capacity, type.Comfort));
                }
                return typesExtensions;
            }
        }
        public List<RoomExtension> GetRooms(TypeRoomExtension type, DateTime start, DateTime end) 
        {
            DateTime truncatedToDaysStartDate = new DateTime(start.Year, start.Month, start.Day);
            DateTime truncatedToDaysEndDate = new DateTime(end.Year, end.Month, end.Day);
            using (HotelModel hm = new HotelModel())
            {
                var req1 = (from room in hm.Room where room.IdTypeRoom == type.Id select room).ToList();
                var req2 = (from room in hm.Room
                            join booking in hm.Booking on room.Id equals booking.IdRoom
                            where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                            room.IdTypeRoom == type.Id && (booking.IdStatus == 1 || booking.IdStatus == 2)
                            select room).ToList();
                var rooms = req1.Except(req2).ToList();
                List<RoomExtension> roomsExtensions = new List<RoomExtension>();
                foreach (var room in rooms)
                {
                    roomsExtensions.Add(new RoomExtension(room, type));
                }
                return roomsExtensions;
            }
        }

        public StringServiceExtension GetAddService(AddService service , int count)
        {
            StringServiceExtension stringServiceExtension;
            using (HotelModel hm = new HotelModel())
            {
               
                var stringService = new StringService();
                stringService.AddService = service;
                stringService.cost = service.cost;
                stringService.count = count;
                stringService.Booking = booking;
                stringServiceExtension = new StringServiceExtension(stringService);
            }
            return stringServiceExtension;
        }

        public double recalculateTotalSum(DateTime startDate , DateTime endDate , RoomExtension selectedRoom , ObservableCollection<StringServiceExtension> enterAddServices , double? discountSize)
        {
            int selectedRoomCost = 0;
            if (selectedRoom != null)
            {
                selectedRoomCost = selectedRoom.cost;
            }
            DateTime truncatedToDaysStartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime truncatedToDaysEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
            TimeSpan nights = truncatedToDaysEndDate - truncatedToDaysStartDate;
            int addServicesSum = 0;
            foreach (var addservice in enterAddServices)
            {
                addServicesSum += addservice.Cost * addservice.Count;
            }
            double totalAmount = (selectedRoomCost * nights.Days + addServicesSum) * (1 - (double)discountSize);
            return totalAmount;
        }

        public void CreateBooking(ObservableCollection<StringServiceExtension> stringServiceExtensions , int IdStatus , int IdUser , DateTime startDate , DateTime endDate, int IdSelectedRoom , double totalSum)
        {
            booking.IdStatus = IdStatus;
            booking.IdUser = IdUser;
            booking.ArrivalDate = startDate;
            booking.DepatureDate = endDate;
            booking.IdRoom = IdSelectedRoom;
            booking.FinalCost = totalSum;
            using (HotelModel hm = new HotelModel())
            {
                hm.Booking.Add(booking);
                foreach (var service in stringServiceExtensions)
                {
                    StringService strService = service.GetStringService();
                    hm.Entry(strService.AddService).State = System.Data.Entity.EntityState.Unchanged;
                    hm.StringService.Add(strService);
                    
                }
                hm.SaveChanges();
            }
        }

        public void SetIdRoom(int id)
        {
            booking.IdRoom = id;
        }

        public void SetFinalCost(double finalCost)
        {
            booking.FinalCost = finalCost;
        }

        public void SetDepatureDate(DateTime depatureDate)
        {
            booking.DepatureDate = depatureDate;
        }

        public void SetArrivalDate(DateTime arrivaldate)
        {
            booking.ArrivalDate = arrivaldate;
        }
    }
}
