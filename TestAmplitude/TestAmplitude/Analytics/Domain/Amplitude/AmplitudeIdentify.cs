using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestAmplitude.Analytics.Domain.Amplitude
{
    public class AmplitudeIdentify
    {

        public AmplitudeIdentify(string api_key, string userId, object userProperties)
        {
            Api_key = api_key;
            identification = new Identification(userId, ToDictionary(userProperties));
        }

        public string Api_key { get; private set; }
        public Identification identification { get; private set; }

        public class Identification
        {
            public Identification(string userId, Dictionary<string, object> userProperties)
            {
                User_Id = userId;
                User_Properties = userProperties;
            }
            public string User_Id { get; set; }
            public Dictionary<string, object> User_Properties { get; set; }
        }
       

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
