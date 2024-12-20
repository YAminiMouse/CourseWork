﻿using DAL.Entities;
using HM2.AdditionalEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace HM2.Model.Admin.SubModel
{
    public class ChangeTypeRoomInformationModel
    {
        public ChangeTypeRoomInformationModel() { }

        public void ChangeInformation(int selectedTypeId , string selectedCost , string selectedDescription , int selectedCapacityId , int selectedComfortId , byte[] data)
        {
            using (HotelModel hm = new HotelModel())
            {
                var st = (from t in hm.TypeRoom where t.Id == selectedTypeId select t).ToList().First();
                st.cost = int.Parse(selectedCost);
                st.description = selectedDescription;
                st.IdSize = selectedCapacityId;
                st.IdComfort = selectedComfortId;
                st.photo = data;
                hm.SaveChanges();
            }
            return;
        }

        public List<ComfortExtension> GetAllComforts()
        {
            List<Comfort> List = new List<Comfort>();
            List<ComfortExtension> comforts = new List<ComfortExtension>();
            using (HotelModel hm = new HotelModel())
            {
                List = (from c in hm.Comfort select c).ToList();
                foreach (var item in List)
                {
                    comforts.Add(new ComfortExtension(item));
                }
            }
            return comforts;
        }

        public List<CapacityExtension> GetAllCapacities()
        {
            List<Capacity> List = new List<Capacity>();
            List<CapacityExtension> capacities = new List<CapacityExtension>();
            using (HotelModel hm = new HotelModel())
            {
                List = (from c in hm.Capacity select c).ToList();
                foreach (var item in List)
                {
                    capacities.Add(new CapacityExtension(item));
                }
            }
            return capacities;
        }

        public ComfortExtension GetComfort(int Id , List<ComfortExtension> comfortList)
        {
            var selectedComfort = comfortList.FirstOrDefault(d =>
            {
                return d.Id == Id;
            });
            return selectedComfort;
        }

        public CapacityExtension GetCapacity(int Id , List<CapacityExtension> capacityList)
        {
            var selectedCapacity = capacityList.FirstOrDefault(d =>
            {
                return d.Id == Id;
            });
            return selectedCapacity;
        }

        public byte[] GetPicture()
        {
            byte[] data = new byte[0];
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    data = File.ReadAllBytes(path);
                }
            }
            return data;
        }

        public BitmapImage UpdateImageSource(byte[] _imageBytes)
        {
            if (_imageBytes == null || _imageBytes.Length == 0)
            {
                return null;
            }

            var bitmap = new BitmapImage();
            var stream = new MemoryStream(_imageBytes);
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            return bitmap;
        }

    }
}
