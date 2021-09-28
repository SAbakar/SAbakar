using System.Linq;
using System.Text;
using System;

namespace Repositories.Divers
{
    // Extension methods must be defined in a static class.
    public static class DateTimeExtensions
    {
        public static bool IsRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
}