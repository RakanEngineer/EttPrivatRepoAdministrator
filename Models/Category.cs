﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EttPrivatRepoAdministrator.Models
{
    class Category
    {
        public Category()
        {

        }
        public Category(string name, string imageURL)
        {
            Name = name;
            ImageUrl = imageURL;
        }

        public Category(int id, string name, string imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
