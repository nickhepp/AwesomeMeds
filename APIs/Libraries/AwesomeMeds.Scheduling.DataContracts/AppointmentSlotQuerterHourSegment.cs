﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Scheduling.DataContracts
{

    /// <summary>
    /// The breakdown of an hour into specific segments.
    /// </summary>
    public enum AppointmentSlotQuerterHourSegment
    {

        FirstQuarterHour = 0,
        SecondQuarterHour = 1,
        ThirdQuarterHour = 2,
        FourthHour = 3

    }
}
