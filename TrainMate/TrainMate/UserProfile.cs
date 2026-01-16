using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrainMate
{
    public  class UserProfile
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string Quote { get; set; }
        public string Status { get; set; }
        public string email { get; set; }

    }
}
