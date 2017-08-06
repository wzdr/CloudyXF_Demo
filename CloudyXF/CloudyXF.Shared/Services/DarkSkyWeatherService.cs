using CloudyXF.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace CloudyXF.Services
{
    public struct DarkSkyWeatherService
    {
        const string APIKey = "---YOUR_OWN_DARK_SKY_KEY---";
        const string webApi = "https://api.darksky.net/forecast/";

        const string authenticatedBaseURL = webApi + APIKey + "/";

        public static string AuthenticatedBaseURL()
        {
            return authenticatedBaseURL;
        }

        public static string WebApi()
        {
            return webApi;
        }
    }

}
