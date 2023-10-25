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

        /// <summary>
        /// The inclusive start of the appointment time window.
        /// </summary>
        public AppointmentSlot StartAppointmentSlot {  get; set; }

        /// <summary>
        /// The exclusive end (this appt slot not included) of the appointment time window.
        /// </summary>
        public AppointmentSlot EndAppointmentSlot { get; set; }

    }
}
