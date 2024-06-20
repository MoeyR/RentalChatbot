using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot.Model
{
    public class Truck: Vehicle, IInsurance
    {
        public string Payload { get; set; }

        public int insuranceCost()
        {
            return 200;
        }

        public int finalTotal() { return base.RentPerDay + this.insuranceCost(); }

        public override string vehicleDetail()
        {
            return $"\n------ TruckID #{CarID} details: ------\n Truck Make: {Make} \n Type: {Type} \n Payload: {Payload} \n Daily Price: {RentPerDay} \n Insurance: {insuranceCost()}";
        }
    }
}
