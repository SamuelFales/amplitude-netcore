using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAmplitude.Analytics.Service
{
    public interface IAmplitudeService
    {
        Task<bool> Identify(string userid, object userProperties);
        Task<bool> LogEvent(string eventName, object eventProperties, object userProperties);
    }


}
