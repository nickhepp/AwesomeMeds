using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Scheduling.Business
{
    public class DateTimeProvider : IDateTimeProvider
    {

        public DateTime GetCurrentDateTimeUtc()
        {
            return DateTime.Now.ToUniversalTime();
        }


    }
}
