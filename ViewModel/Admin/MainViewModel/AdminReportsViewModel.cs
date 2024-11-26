using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using HM2.Command;
using HM2.Model.Admin.MainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HM2.ViewModel.Admin
{
    public class AdminReportsViewModel : AdminViewModel
    {
        private AdminReportsModel adminReportsModel;
        public AdminReportsViewModel(WindowContext windowContext) 
        {
            Debug.WriteLine("Создан AdminReportsViewModel");
            adminReportsModel = new AdminReportsModel();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            CreateReport = new RelayCommand(_ =>
            {
                //DateTime truncatedToDaysStartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
                //DateTime truncatedToDaysEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);
                //using (HotelModel hm = new HotelModel())
                //{
                //    var allRooms = (from room in hm.Room where room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null) select room).ToList();

                //    var busyRooms = (from room in hm.Room
                //                     join booking in hm.Booking on room.Id equals booking.IdRoom
                //                     where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                //                     (booking.IdStatus == 2) &&
                //                     (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                //                     select room).ToList();

                //    var bookings = (from room in hm.Room
                //                        join booking in hm.Booking on room.Id equals booking.IdRoom
                //                        where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                //                        (booking.IdStatus == 1) &&
                //                        (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                //                        select booking).ToList();

                //    var busyRoomsBooking = (from booking in hm.Booking
                //                            join room in hm.Room on booking.IdRoom equals room.Id
                //                            where booking.ArrivalDate <= truncatedToDaysEndDate && booking.DepatureDate >= truncatedToDaysStartDate &&
                //                (booking.IdStatus == 2) &&
                //                (room.CreateDate <= truncatedToDaysStartDate && ((room.DeleteDate != null && truncatedToDaysEndDate < room.DeleteDate) || room.DeleteDate == null))
                //                            select booking).ToList();
                //    //int countBusyRoomsBooking = busyRoomsBooking.Count;
                //    double totalSumStringService = 0.0;
                //    double totalSumBooking = 0.0;
                //    foreach (var item in busyRoomsBooking)
                //    {
                //        BusyRoomsBooking.Add(new UserBookingExtension(item));
                //        totalSumBooking += item.Room.TypeRoom.cost * (EndDate - StartDate).Days;
                //        var stringServiceList = (from str in hm.StringService where str.IdBooking == item.Id select str).ToList();
                //        foreach (var stringService in stringServiceList)
                //        {
                //            totalSumStringService += stringService.cost;
                //        }
                //    }
                //    foreach(var item in bookings)
                //    {
                //        WaitRoomsBooking.Add(new UserBookingExtension(item));
                //    }
                    Report report = adminReportsModel.GetData(StartDate, EndDate);
                    foreach(var item in report.BusyRoomsBooking)
                    {
                        BusyRoomsBooking.Add(item);
                    }
                    foreach (var item in report.Bookings)
                    {
                        WaitRoomsBooking.Add(item);
                    }
                    RevenueRooms = report.TotalSumBooking.ToString();
                    RevenueAddService = report.TotalSumStringService.ToString();
                    CountRooms = report.AllRooms.Count.ToString();
                    CountBusyRooms = report.BusyRooms.Count.ToString();
                    CountBookings = report.Bookings.Count.ToString();
                //}
            })
            {

            };
        }

    }
}
