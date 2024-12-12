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
            adminReportsModel = new AdminReportsModel();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            CreateReport = new RelayCommand(_ =>
            {
                Report report = null;
                try
                {
                    report = adminReportsModel.GetData(StartDate, EndDate);
                }
                catch(Exception ex)  
                {
                    MessageBox.Show(ex.Message);
                }
                if (report == null)
                {
                    return;
                }
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
                try
                {
                    PdfDocument pdfDocument = adminReportsModel.PreparePDFReport(StartDate, EndDate, RevenueRooms, RevenueAddService, CountRooms, CountBusyRooms, CountBookings);
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pdfDocument.Save(saveFileDialog.FileName);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            CreateReportCommand = new RelayCommand(_ =>
            {
                TopThreeReport report = null;
                try
                {
                    report = adminReportsModel.GetTopThreeServices();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Service1Name = report.FirstName;
                Service2Name = report.SecondName;
                Service3Name = report.ThirdName;
                Service1Earnings = report.FirstCost.ToString();
                Service2Earnings = report.SecondCost.ToString();
                Service3Earnings = report.ThirdCost.ToString();
            });

            ExportReportCommand = new RelayCommand(_ =>
            {
                try
                {
                    int first = int.Parse(Service1Earnings);
                    int second = int.Parse(Service2Earnings);
                    int third = int.Parse(Service3Earnings);
                    PdfDocument pdfDocument = adminReportsModel.PrepareTopThreePdfDocument(first, second, third, Service1Name, Service2Name, Service3Name);
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pdfDocument.Save(saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
