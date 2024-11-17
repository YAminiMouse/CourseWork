using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.AdditionalEntities
{
    public class CapacityExtension
    {
        public int Id { get; set; }
        public string name { get; set; }
        public CapacityExtension(Capacity capacity)
        {
            Id = capacity.Id;
            name = capacity.name;
        }
    }
}
