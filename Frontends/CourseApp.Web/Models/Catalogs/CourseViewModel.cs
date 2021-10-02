﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Models.Catalogs
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public string ShortPictureUrl { get; set; }


        public DateTime CreatedTime { get; set; }

        public FeatureViewModel Feature { get; set; }


        public string CategoryId { get; set; }


        public CategoryViewModel Category { get; set; }

        public IFormFile PhotoFormFile { get; set; }
    }
}
