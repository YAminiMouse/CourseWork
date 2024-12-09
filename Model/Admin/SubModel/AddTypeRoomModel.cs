using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HM2.Model.Admin.SubModel
{
    public class AddTypeRoomModel
    {
        public AddTypeRoomModel() { }

        public void AddNewTypeRoom(string cost, int selectedComfortId, int selectedCapacityId, string selectedCapacityName, string selectedComfortName, string description, byte[] picture)
        {
            var costType = int.Parse(cost);
            using (HotelModel hm = new HotelModel())
            {
                TypeRoom typeRoom = new TypeRoom();
                typeRoom.cost = costType;
                typeRoom.IdComfort = selectedComfortId;
                typeRoom.IdSize = selectedCapacityId;
                typeRoom.description = description;
                typeRoom.photo = picture;
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

        public Tuple<BitmapImage , byte[]> UpdateImageSource()
        {
            var bitmap = new BitmapImage();
            byte[] _imageBytes = new byte[0];
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    _imageBytes = File.ReadAllBytes(path);
                    var stream = new MemoryStream(_imageBytes);
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
            }
            return Tuple.Create(bitmap, _imageBytes);
        }
    }
}
