using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class AddServiceExtension
    {
        public int Id { get; set; }

        public int cost { get; set; }
        public string name { get; set; }

        public AddServiceExtension(AddService addService)
        {
            Id = addService.Id;
            cost = addService.cost;
            name = addService.name;
        }
    }
}
