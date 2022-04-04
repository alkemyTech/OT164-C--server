﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class NewsDTO
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public int CategoriesId { get; set; }

    }
}
