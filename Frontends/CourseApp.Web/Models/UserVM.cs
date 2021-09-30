﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }


        public IEnumerable<string> GetUserProps()
        {
            yield return UserName;
            yield return Email;
            yield return City;

        }
    }

}
