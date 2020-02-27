using System;
using System.Collections.Generic;
using System.Reflection;
using TestAmplitude.Analytics.Domain.Amplitude.Extesions;

namespace TestAmplitude.Analytics.Domain.Amplitude
{

    public class AmplitudeEvent
    {
        public AmplitudeEvent(string api_key, Event _event)
        {
            Api_key = api_key;
            Events = new List<Event>
            {
                _event
            };
        }

        public string Api_key { get; set; }
        public List<Event> Events { get; set; }

    }


    public class Event
    {
        public Event(long sessionId,string eventName, object eventProperties, object userProperties)
        {
            User_id = "qa.samuelfales";
            Time = DateTime.UtcNow.ToUnixEpoch();
            Event_type = Uri.EscapeUriString(eventName);
            Event_properties = ToDictionary(eventProperties);
            User_properties = ToDictionary(userProperties);
            Session_id = sessionId;
            Platform = "Web";
        }

        public string User_id { get; set; }
        public string Event_type { get; set; }
        public long Time { get; set; }
        public Dictionary<string, object> Event_properties { get; set; }
        public Dictionary<string, object> User_properties { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public string Ip { get; set; }
        public string Idfa { get; set; }
        public int Event_id { get; set; }
        public long Session_id { get; set; }
        public string Platform { get; set; }

        static internal Dictionary<string, object> ToDictionary<T>(T obj)
        {
            if (obj != null)
            {
                var amplitudeProps = new Dictionary<string, object>();

                foreach (PropertyInfo property in obj.GetType().GetRuntimeProperties())
                {
                    amplitudeProps[property.Name] = property.GetValue(obj, null);
                }

                if (amplitudeProps.Count != 0)
                {
                    return amplitudeProps;
                }
            }

            return null;
        }

    }

}
