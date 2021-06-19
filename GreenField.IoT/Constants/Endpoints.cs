using System.IO;

namespace GreenField.IoT.Constants
{
    public class Endpoints
    {
        public static readonly string AuthUrlPath = Configuration.APIUrlBase + "/auth/login";
        public static readonly string GetAllFields = Configuration.APIUrlBase + "/field";
        public static readonly string GetField = Configuration.APIUrlBase + "/field";
        public static readonly string GetAllPests = Configuration.APIUrlBase + "/pest";
        public static readonly string GetAllWeeds = Configuration.APIUrlBase + "/weed";
    }
}
