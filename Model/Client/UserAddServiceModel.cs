using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class UserAddServiceModel
    {
        public UserAddServiceModel() { }

        public List<StringServiceExtension> GetStrServices(int id)
        {
            List<StringServiceExtension> addServices = new List<StringServiceExtension>();
            using (HotelModel hm = new HotelModel())
            {
                var servicesList = (from strService in hm.StringService where strService.IdBooking == id select strService).ToList();
                foreach(var service in servicesList)
                {
                    addServices.Add(new StringServiceExtension(service));
                }
            }
            return addServices;
        }
    }
}
