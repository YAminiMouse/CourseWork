using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using HM2.Command;
using HM2.Model.Admin.MainModel;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Report report = adminReportsModel.GetData(StartDate, EndDate);
                foreach (var item in report.BusyRoomsBooking)
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
            });

            ExportReport = new RelayCommand(_ =>
            {
                PdfDocument pdfDocument = adminReportsModel.PreparePDFReport(StartDate , EndDate , RevenueRooms , RevenueAddService , CountRooms , CountBusyRooms , CountBookings);
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pdfDocument.Save(saveFileDialog.FileName);
                }
            });
        }
    }
}
