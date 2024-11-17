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
                list = (from a in hm.AddService select a).ToList();
            }
            return list;
        }
    }
}
