using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class RoomExtension
    {
        public int Id { get; set; }

        public int number { get; set; }

        public int floor { get; set; }

        public int IdTypeRoom { get; set; }
        public int cost { get; set; }
        public RoomExtension(Room room , TypeRoomExtension type) 
        { 
            Id = room.Id;
            number = room.number;
            floor = room.floor;
            IdTypeRoom = room.IdTypeRoom;
            cost = type.cost;
        }
    }
}
