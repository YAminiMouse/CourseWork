using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.SubModel
{
    public class ChangeServiceInformationModel
    {
        public ChangeServiceInformationModel() { }

        public void EditAddService(int Id , string selectedCost , string selectedName)
        {
            using (HotelModel hm = new HotelModel())
            {
                var addService = (from a in hm.AddService where a.Id == Id select a).ToList().First();
                addService.cost = int.Parse(selectedCost);
                addService.name = selectedName;
                hm.SaveChanges();
            }
            return;
        }
    }
}
