using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class ShipmentModel
    {
        public int OrderId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }
        public DateTime? ActualArrivalDate { get; set; }

    }
}
