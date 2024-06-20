using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot.Model
{
    public class Car : Vehicle, IInsurance
    {
        public int Year { get; set; }

        public int insuranceCost()
        {
            return 100;
        }

        public int finalTotal() { return base.RentPerDay + this.insuranceCost(); }

        public override string vehicleDetail()
        {
            return $"\n------ CarID #{CarID} details: ------\n Car Make: {Make} \n Type: {Type} \n Year: {Year} \n Daily Price: {RentPerDay} \n Insurance: {insuranceCost()}";
        }
    }

}
