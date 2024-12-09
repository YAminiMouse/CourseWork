using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace DAL.AdditionalEntities
{
    public class RoomExtension
    {
        public int Id { get; set; }

        public int number { get; set; }

        public int floor { get; set; }

        public int IdTypeRoom { get; set; }
        public string NameTypeRoom { get; set; }
        public int cost { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime? IsDeleted { get; set; }
        public BitmapImage ImagePath { get; set; }
        public RoomExtension(Room room , TypeRoomExtension type) 
        { 
            Id = room.Id;
            number = room.number;
            floor = room.floor;
            IdTypeRoom = room.IdTypeRoom;
            NameTypeRoom = type.name;
            cost = type.cost;
            IsCreated = (DateTime)room.CreateDate;
            IsDeleted = room.DeleteDate;
            ImagePath = new BitmapImage();
            if (type.data != null && type.data.Length > 0)
            {
                ImagePath.BeginInit();
                ImagePath.StreamSource = new MemoryStream(type.data);
                ImagePath.EndInit();
            }
            else
            {
                ImagePath = new BitmapImage();
            }
            
        }
    }
}
