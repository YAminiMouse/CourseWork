using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.SubModel
{
    public class AddNewServiceModel
    {
        public AddNewServiceModel() { }

        public void ConfirmService(string cost , string name)
        {
            var costService = int.Parse(cost);
            using (HotelModel hm = new HotelModel())
            {
                AddService addService = new AddService();
                addService.cost = costService;
                addService.name = name;
                hm.AddService.Add(addService);
                hm.SaveChanges();
            }
            return;
        }
    }
}
