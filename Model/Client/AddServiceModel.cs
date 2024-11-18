using DAL.Entities;
using HM2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace HM2.Model
{
    public class AddServiceModel
    {
        public List<AddService> GetAddServices(/*ReturnAddServices callBack*/)
        {
            List<AddService> list = new List<AddService>();
            using (HotelModel hm = new HotelModel())
            {
                var addServices = (from service in hm.AddService where service.deleteDate == null select service).ToList();
                foreach (AddService service in addServices)
                {
                    list.Add(service);
                }
                return list;
            }
        }
    }
}
