using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestAmplitude.Analytics.Domain.Amplitude.Extesions
{
    static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a DateTime to unix time (in ms).
        /// </summary>
        /// <param name="dateTime">A DateTime to convert to epoch time (note, will be converted to UTC).</param>
        /// <returns># of milliseconds since 1/1/1970</returns>
        public static long ToUnixEpoch(this DateTime dateTime) => (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

   }
}
