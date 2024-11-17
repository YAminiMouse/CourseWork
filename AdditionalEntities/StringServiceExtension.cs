using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.AdditionalEntities
{
    public class StringServiceExtension
    {
        private StringService _stringService;

        public string Name { get; set; }
        public int Count { get; set; }

        public int Cost { get; set; }
        public StringService GetStringService()
        {
            return _stringService;
        }
        public StringServiceExtension(StringService stringService) 
        {
            _stringService = stringService;
            Name = _stringService.AddService.name;
            Count = _stringService.count;
            Cost = _stringService.cost;
        }
    }
}