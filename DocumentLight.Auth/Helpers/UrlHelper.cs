using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DocumentLight.Auth.Helpers
{
    internal static class UrlHelper
    {
        public static string AddUrlParameters(string url, IDictionary<string, string> parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var parameter in parameters)
                query[parameter.Key] = parameter.Value;

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
