using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class DiscountExtension
    {
        public int Id { get; set; }

        public string name { get; set; }

        public double size { get; set; }

        public DiscountExtension(Discount discount) 
        {
            Id = discount.Id;
            name = discount.name;
            size = discount.size;
        }
    }
}
