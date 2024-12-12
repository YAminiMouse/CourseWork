using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OxyPlot.Series;
using OxyPlot;

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

        public PdfDocument PreparePDFReport(DateTime start, DateTime end, string revenueRooms, string revenueAddService, string countRooms, string countBusyRooms, string countBookings)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Отчет";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyleEx.Italic);
            XTextFormatter tf = new XTextFormatter(gfx);
            string text = "                                    Отчет\n\n\n\n" +
            string.Format("  За период с {0} по {1}\n\n\n", start.ToString("dd/MM/yyyy"), end.ToString("dd/MM/yyyy")) +
            string.Format("  Общая информация:\n\n") +
            string.Format("          Общее количество номеров: {0}\n", countRooms) +
            string.Format("          Занятых номеров: {0}\n", countBusyRooms) +
            string.Format("          Количество броней: {0}\n\n\n\n", countBookings) +
            string.Format("  Выручка:\n\n") +
            string.Format("          Выручка за занятые номера: {0} руб.\n", revenueRooms) +
            string.Format("          Выручка за услуги: {0} руб.\n", revenueAddService);
            tf.DrawString(text, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopLeft);

            int allRooms = int.Parse(countRooms);
            int busyRoomsCount = int.Parse(countBusyRooms);
            int bookedRoomsCount = int.Parse(countBookings);
            int freeRoomsCount = allRooms - busyRoomsCount - bookedRoomsCount;

            byte[] chartBytes = GeneratePieChartBytes(freeRoomsCount, busyRoomsCount, bookedRoomsCount);
            using (var chartStream = new MemoryStream(chartBytes))
            {
                XImage chartImage = XImage.FromStream(chartStream);
                gfx.DrawImage(chartImage, 50, 490, 500, 300); // Позиция и размеры диаграммы
            }

            return document;
        }

        private byte[] GeneratePieChartBytes(int freeRooms, int busyRooms, int bookedRooms)
        {
            var plotModel = new PlotModel { Title = "Rooms" };

            var pieSeries = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0,
            };

            // Добавляем данные в диаграмму
            pieSeries.Slices.Add(new PieSlice("FreeRooms", freeRooms) { Fill = OxyColors.Green });
            pieSeries.Slices.Add(new PieSlice("BusyRooms", busyRooms) { Fill = OxyColors.Red });
            pieSeries.Slices.Add(new PieSlice("BookedRooms", bookedRooms) { Fill = OxyColors.Orange });

            plotModel.Series.Add(pieSeries);
            plotModel.DefaultFont = "Arial";
            plotModel.DefaultFontSize = 40;

            // Экспорт в поток
            using (var stream = new MemoryStream())
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(plotModel, stream);
                return stream.ToArray();
            }
        }

        public TopThreeReport GetTopThreeServices()
        {
            TopThreeReport report = null;
            using (var hm = new HotelModel())
            {
                var topServices = hm.StringService
                    .Join(
                        hm.AddService,
                        ss => ss.IdAddService,
                        asv => asv.Id,
                        (ss, asv) => new
                        {
                            AddServiceName = asv.name,
                            Revenue = ss.count * ss.cost
                        })
                    .GroupBy(x => x.AddServiceName)
                    .Select(g => new
                    {
                        ServiceName = g.Key,
                        TotalRevenue = g.Sum(x => x.Revenue)
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .Take(3)
                    .ToList();
                report = new TopThreeReport(topServices[0].TotalRevenue , topServices[1].TotalRevenue, topServices[2].TotalRevenue , topServices[0].ServiceName , topServices[1].ServiceName , topServices[2].ServiceName);
            }
            return report;
        }

        public PdfDocument PrepareTopThreePdfDocument(int firstCost, int secondCost, int thirdCost, string firstName, string secondName, string thirdName)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Отчет";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont headerFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
            XFont tableFont = new XFont("Verdana", 12, XFontStyleEx.Regular);

            gfx.DrawString("Топ 3 услуги", headerFont, XBrushes.Black, new XRect(0, 20, page.Width, 50), XStringFormats.TopCenter);

            double startX = 50;
            double startY = 80;
            double cellHeight = 25;
            double columnWidth = page.Width / 2 - 60;

            gfx.DrawRectangle(XPens.Black, startX, startY, columnWidth, cellHeight);
            gfx.DrawRectangle(XPens.Black, startX + columnWidth, startY, columnWidth, cellHeight);

            gfx.DrawString("Название услуги", tableFont, XBrushes.Black, new XRect(startX, startY, columnWidth, cellHeight), XStringFormats.Center);
            gfx.DrawString("Заработано", tableFont, XBrushes.Black, new XRect(startX + columnWidth, startY, columnWidth, cellHeight), XStringFormats.Center);

            string[] services = { firstName, secondName, thirdName };
            int[] costs = { firstCost, secondCost, thirdCost };

            for (int i = 0; i < 3; i++)
            {
                double currentY = startY + (i + 1) * cellHeight;

                gfx.DrawRectangle(XPens.Black, startX, currentY, columnWidth, cellHeight);
                gfx.DrawRectangle(XPens.Black, startX + columnWidth, currentY, columnWidth, cellHeight);

                gfx.DrawString(services[i], tableFont, XBrushes.Black, new XRect(startX + 10, currentY, columnWidth, cellHeight), XStringFormats.CenterLeft);
                gfx.DrawString(costs[i].ToString("C"), tableFont, XBrushes.Black, new XRect(startX + columnWidth - 10, currentY, columnWidth, cellHeight), XStringFormats.CenterRight);
            }

            byte[] chartBytes = GenerateBarChartBytes(firstCost, secondCost, thirdCost, firstName, secondName, thirdName);
            using (var chartStream = new MemoryStream(chartBytes))
            {
                XImage chartImage = XImage.FromStream(chartStream);
                gfx.DrawImage(chartImage, 50, 300, 500, 300);
            }

            return document;
        }

        private byte[] GenerateBarChartBytes(int firstCost, int secondCost, int thirdCost, string firstName, string secondName, string thirdName)
        {
            var plotModel = new PlotModel { Title = "Top 3 Services" };

            var barSeries = new BarSeries
            {
                ItemsSource = new[]
                {
                    new BarItem { Value = firstCost },
                    new BarItem { Value = secondCost },
                    new BarItem { Value = thirdCost }
                },
                LabelPlacement = LabelPlacement.Outside,

            };
            plotModel.Series.Add(barSeries);

            using (var stream = new MemoryStream())
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(plotModel, stream);
                return stream.ToArray();
            }
        }

    }
}