using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenSupport
{
    public class recipe
    {
        public string recipeName { get; set; }
        public string[] smallImageUrls { get; set; }
        public int id { get; set; }
        public string yummly_id { get; set; }
        public int rating { get; set; }
        public string[] ingredients { get; set; }
        public int? totalTimeInSeconds { get; set; }
        public int? favorites { get; set; }

    }
}
