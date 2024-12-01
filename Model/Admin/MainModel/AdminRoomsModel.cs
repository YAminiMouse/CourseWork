using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.MainModel
{
    public class AdminRoomsModel
    {
        public AdminRoomsModel() { }

        public List<RoomExtension> GetRooms()
        {
            var rooms = new List<Room>();
            var list = new List<RoomExtension>();
            using (HotelModel hm = new HotelModel())
            {
                rooms = (from r in hm.Room select r).ToList();
                foreach (var item in rooms)
                {
                    var itemTypeRoom = new TypeRoomExtension(item.TypeRoom, item.TypeRoom.Capacity, item.TypeRoom.Comfort);
                    list.Add(new RoomExtension(item, itemTypeRoom));
                }
            }
            
            return list;
        }

        public void DeleteSelectedRoom(int selectedRoomId)
        {
            using (HotelModel hm = new HotelModel())
            {
                var room = (from r in hm.Room where r.Id == selectedRoomId select r).ToList().First();
                room.DeleteDate = DateTime.Now;
                hm.SaveChanges();
            }
        }
    }
}
