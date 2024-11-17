using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class TypeRoomExtension
    {
        public int Id { get; set; }

        public int cost { get; set; }

        public string description { get; set; }

        public string name { get; set; }
        public string comfortLevel { get; set; }
        public int IdComfort { get; set; }
        public int IdCapacity { get; set; }
        public string capacity { get; set; }

        public TypeRoomExtension(TypeRoom type, Capacity capacity , Comfort comfort)
        { 
            Id = type.Id;
            cost = type.cost;
            description = type.description;
            name = capacity.name + " " +comfort.name;
            comfortLevel = comfort.name;
            IdCapacity = capacity.Id;
            IdComfort = comfort.Id;
            this.capacity = capacity.name;
        }
    }
}
