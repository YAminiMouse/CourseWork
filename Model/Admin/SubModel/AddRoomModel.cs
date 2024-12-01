using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HM2.Model.Admin.SubModel
{
    public class AddRoomModel
    {
        public AddRoomModel() { }

        public void AddNewRoom(string number , string floor , int selectedTypeId)
        {
            var roomNumber = int.Parse(number);
            var floorNumber = int.Parse(floor);
            using(HotelModel hm = new HotelModel())
            {
                Room room = new Room();
                room.number = roomNumber;
                room.floor = floorNumber;
                room.IdTypeRoom = selectedTypeId;
                room.CreateDate = DateTime.Now;
                hm.Room.Add(room);
                hm.SaveChanges();
            }
            return;
        }

        public List<TypeRoomExtension> GetTypes()
        {
            List<TypeRoom> list = new List<TypeRoom>();
            List<TypeRoomExtension> types = new List<TypeRoomExtension>();
            using (HotelModel hm = new HotelModel())
            {
                list = (from t in hm.TypeRoom select t).ToList();
                foreach (var item in list)
                {
                    types.Add(new TypeRoomExtension(item , item.Capacity , item.Comfort));
                }
            }
            return types;
        }
    }
}
