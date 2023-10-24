using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Providers.DataContracts
{
    /// <summary>
    /// The times at which a provider is available to host appointments.
    /// </summary>
    public class ProviderAvailableTimeWindow
    {

        public Guid ProviderId { get; set; }

        public AppointmentSlot StartAppointmentSlot {  get; set; }

        public AppointmentSlot EndAppointmentSlot { get; set; }

    }
}
