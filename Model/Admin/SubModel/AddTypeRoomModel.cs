using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.SubModel
{
    public class AddTypeRoomModel
    {
        public AddTypeRoomModel() { }

        public void AddNewTypeRoom(string cost , int selectedComfortId , int selectedCapacityId , string selectedCapacityName , string selectedComfortName , string description)
        {
            var costType = int.Parse(cost);
            using (HotelModel hm = new HotelModel())
            {
                TypeRoom typeRoom = new TypeRoom();
                typeRoom.cost = costType;
                typeRoom.IdComfort = selectedComfortId;
                typeRoom.IdSize = selectedCapacityId;
                //typeRoom.name = selectedCapacityName + " " + selectedComfortName;
                typeRoom.description = description;
                hm.TypeRoom.Add(typeRoom);
                hm.SaveChanges();
            }
            return;
        }

        public List<CapacityExtension> GetAllCapacities()
        {
            List<Capacity> list = new List<Capacity>();
            List<CapacityExtension> capacities = new List<CapacityExtension>();
            using (HotelModel hm = new HotelModel())
            {
                list = (from c in hm.Capacity select c).ToList();
                foreach (var item in list)
                {
                    capacities.Add(new CapacityExtension(item));
                }
            }
            return capacities;
        }

        public List<ComfortExtension> GetAllComforts()
        {
            List<Comfort> list = new List<Comfort>();
            List<ComfortExtension> comforts = new List<ComfortExtension>();
            using (HotelModel hm = new HotelModel())
            {
                list = (from c in hm.Comfort select c).ToList();
                foreach (var item in list)
                {
                    comforts.Add(new ComfortExtension(item));
                }
            }
            return comforts;
        }
    }
}
