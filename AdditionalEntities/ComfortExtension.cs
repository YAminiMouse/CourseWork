using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.AdditionalEntities
{
    public class ComfortExtension
    {
        public int Id { get; set; }
        public string name { get; set; }
        public ComfortExtension(Comfort comfort) 
        {
            Id = comfort.Id;
            name = comfort.name;
        }

    }
}
