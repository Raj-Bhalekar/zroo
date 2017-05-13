using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BSWebApp.Common
{
    public class CommonAjaxCallToWebAPI
    {


        public string AjaxGet(string url, List<KeyValuePair<string, string>> paramList )
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                string finalUrl = url;

                if (paramList != null && paramList.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("?");
                    foreach (var param in paramList)
                    {
                        sb.Append(param.Key);
                        sb.Append("=");
                        sb.Append(param.Value);
                    }

                    finalUrl = finalUrl + sb.ToString();
                }


                HttpResponseMessage response = client.GetAsync(finalUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rslt = response.Content.ReadAsStringAsync().Result;
                    return rslt;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
