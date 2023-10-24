using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Scheduling.DataContracts.Requests
{
    public class ReserveAppointmentSlotRequest
    {

        public Guid ClientID { get; set; }  

        public AppointmentSlot AppointmentSlot { get; set; }

    }
}
