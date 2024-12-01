using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.SubModel
{
    public class ChangeRoomInformationModel
    {
        public List<TypeRoomExtension> GetTypes()
        {
            var list = new List<TypeRoomExtension>();
            using (HotelModel hm = new HotelModel())
            {
                var types = (from t in hm.TypeRoom select t).ToList();
                foreach (var item in types)
                {
                    list.Add(new TypeRoomExtension(item, item.Capacity, item.Comfort));
                }
            }
            return list;

        }

        public TypeRoomExtension GetType(int Id, List<TypeRoomExtension> typeList)
        {
            var selectedType = typeList.FirstOrDefault(d =>
            {
                return d.Id == Id;
            });
            return selectedType;

        }

        public void ChangeInformation(int selectedRoomId , int selectedTypeId, string number , string floor)
        { 
            using (HotelModel hm = new HotelModel())
            {
                var room = (from r in hm.Room where r.Id == selectedRoomId select r).ToList().First();
                room.number = int.Parse(number);
                room.floor = int.Parse(floor);
                room.IdTypeRoom = selectedTypeId;
                hm.SaveChanges();
            }
            return;
        }
    }
}
