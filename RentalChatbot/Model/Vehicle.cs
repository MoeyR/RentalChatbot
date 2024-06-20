using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot.Model
{
    public abstract class Vehicle
    {
        public int CarID { get; set; }
        public string Make { get; set; }
        public string Type { get; set; }
        public int RentPerDay { get; set; }

        public virtual string vehicleDetail() { return "Details"; }
    }
}
