using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace liftoff_storefront.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public List<UserComment> Comments { get; set; }

        public Product()
        {
        }

        public Product(string name, string imageURL, string description)
        {
            Name = name;
            ImageURL = imageURL;
            Description = description;
        }
    }
}

