using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestAmplitude.Analytics.Domain.Amplitude;
using TestAmplitude.Analytics.Domain.Amplitude.Extesions;

namespace TestAmplitude.Analytics.Service
{
    public class AmplitudeService : IAmplitudeService
    {
        private const string API_Key = "6d19be97cbc36429783a30306f7b35cf";
        private readonly bool _shouldTrackEvents;
        private AmplitudeIdentify identification;
        private long sessionId;

        public AmplitudeService()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _shouldTrackEvents = env == "Prod" || env == "Preprod";
            StartNewSession();
        }

        private void StartNewSession()
        {
            sessionId = DateTime.UtcNow.ToUnixEpoch();
        }

        public async Task<bool> Identify(string userid,  object userProperties)
        {
            identification = new AmplitudeIdentify(API_Key, userid, userProperties);
            return await IdentifyAsync(identification);

        }

        public async Task<bool> LogEvent(string eventName, object eventProperties, object userProperties)
        {

            AmplitudeEvent amplitudeEvent = new AmplitudeEvent(API_Key, new Event(sessionId,eventName, eventProperties, userProperties));

            string data = JsonConvert.SerializeObject(amplitudeEvent, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).ToLower();
            return await SaveAsync("2/httpapi", data);
        }


        private async Task<bool> IdentifyAsync(AmplitudeIdentify amplitudeIdentify)
        {

            string identification = JsonConvert.SerializeObject(amplitudeIdentify.identification, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).ToLower();
            try
            {

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.amplitude.com/identify"))
                    {
                        var contentList = new List<string>();
                        contentList.Add($"api_key={amplitudeIdentify.Api_key}");
                        contentList.Add($"identification=[{identification}]");
                        request.Content = new StringContent(string.Join("&", contentList));
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                        var postResult = await httpClient.SendAsync(request);
                        return postResult.IsSuccessStatusCode;
                    }
                }
            }
            catch 
            {
                return false;
            }
        }

        private async Task<bool> SaveAsync(string endPoint, string paramData)
        {

            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://api.amplitude.com/{endPoint}")
                {
                    Content = new StringContent(paramData)
            };

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var postResult = await httpClient.SendAsync(request);
                return postResult.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }

    }
}
