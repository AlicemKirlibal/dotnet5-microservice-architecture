using System;
using System.Collections.Generic;
using System.Text;

namespace CourseApp.Shared.Messages
{
   public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdatedName { get; set; }
    }
}
