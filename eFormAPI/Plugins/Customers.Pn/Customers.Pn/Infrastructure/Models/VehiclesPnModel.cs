using System.Collections.Generic;

namespace Customers.Pn.Infrastructure.Models
{
    public class VehiclesPnModel
    {
        public int Total { get; set; }
        public List<VehiclePnModel> Vehicles { get; set; }

        public VehiclesPnModel()
        {
            Vehicles = new List<VehiclePnModel>();
        }
    }
}
