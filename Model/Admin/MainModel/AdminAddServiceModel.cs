using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.MainModel
{
    public class AdminAddServiceModel
    {
        public AdminAddServiceModel() { }

        public List<AddService> GetAllGetService()
        {
            var list = new List<AddService>();
            using(HotelModel hm = new HotelModel())
            {
                list = (from a in hm.AddService where a.deleteDate == null select a).ToList();
            }
            return list;
        }

        public void DeleteSelectedService(int selectedServiceId)
        {
            using(HotelModel hm = new HotelModel())
            {
                var service = (from a in hm.AddService where a.Id == selectedServiceId select a).ToList().First();
                service.deleteDate = DateTime.Now;
                hm.SaveChanges();
            }
        }
    }
}
