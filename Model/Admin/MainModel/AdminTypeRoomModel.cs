using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin
{
    public class AdminTypeRoomModel
    {
        public AdminTypeRoomModel() { }

        public List<TypeRoomExtension> GetAllTypes()
        {
            List<TypeRoomExtension> allTypes = new List<TypeRoomExtension>();
            using (HotelModel hm = new HotelModel())
            {
                List<TypeRoom> types = (from t in hm.TypeRoom select t).ToList();
                foreach (TypeRoom t in types)
                {
                    allTypes.Add(new TypeRoomExtension(t, t.Capacity, t.Comfort));
                }
            }
            return allTypes;
        }
    }
}
