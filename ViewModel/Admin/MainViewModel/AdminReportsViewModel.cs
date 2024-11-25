using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using System;
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
        public AdminReportsViewModel(WindowContext windowContext) 
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            CreateReport = new RelayCommand(_ =>
            {
                Debug.WriteLine("btn create report is pressed");
            });

            //using (HotelModel hm = new HotelModel())
            //{
            //    var allRooms = (from count );
            //}
            
        }

    }
}
