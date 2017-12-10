﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace BSWebApp.Common
{
    public class CommonAjaxCallToWebAPI
    {


        public string AjaxGet(string url, List<KeyValuePair<string, string>> paramList)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                    "text/html,application/xhtml+xml,application/xml");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"application/json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"application/xml"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/x-json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/javascript"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/xml"));
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
                        sb.Append("&");
                    }

                    finalUrl = finalUrl + sb.ToString().Substring(0, sb.ToString().Length - 1);
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

        public async Task<HttpResponseMessage> AjaxPost(string postUrl,object model)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

                client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                    "text/html,application/xhtml+xml,application/xml");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"application/json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"application/xml"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/x-json"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/javascript"));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(@"text/xml"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                response = client.PostAsync(postUrl, content).Result;
                return response;
            }

        }
    }
}