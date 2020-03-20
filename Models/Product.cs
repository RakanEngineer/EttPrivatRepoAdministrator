using System;
using System.Collections.Generic;
using System.Text;

namespace EttPrivatRepoAdministrator.Models
{
    class Product
    {
        public Product()
        {

        }
        public Product(string name, string description, int price, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public Product(int id, string name, string description, int price, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public IList<CategoryProduct> CategoryProduct { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}