// ***********************************************************************
// Assembly         : 
// Author           : sanjay
// Created          : 28-dec-2020
//**********************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CPDMSEF;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CPDMS.Models.Utility
{

    public class HttpWebClients:IHttpWebClients
    {
        private readonly ILogger<HttpWebClients> _logger;

        private readonly IOptions<KeyList> _objKeyList;
        public HttpWebClients(IOptions<KeyList> objKeyList, ILogger<HttpWebClients> logger)
        {
            _objKeyList = objKeyList;
            _logger = logger;

        }
        public string PostRequest(string URI, string parameterValues)
        {
            
            string BaseURI = _objKeyList.Value.WebApiurl;
            string URL = BaseURI + URI;
            string jsonString = null;
           var jwt = GetJwt();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZURTZWN1cml0eTplX0RfQVBJLXVyaQ==");
                client.Timeout= TimeSpan.FromMinutes(20);
                //GET Method  
                HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, c).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   //.Replace("\r\n", "'")
                                                   .Trim(new char[1] { '"' });
                }

            }
            return jsonString;
        }
       
        public string GetRequest(string URI, object parameterValues)
        {
            string BaseURI = _objKeyList.Value.WebApiurl;

            string URL = BaseURI + URI;
            string jsonString = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZURTZWN1cml0eTplX0RfQVBJLXVyaQ==");
                //GET Method  
                HttpResponseMessage response = client.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   .Trim(new char[1] { '"' });

                }
            }
            return jsonString;
        }
        //public static string PostRequest(string URI, string parameterValues)
        //{
        //    string BaseURI = "https://localhost:44363/api/";
        //    string URL = BaseURI + URI;
        //    string jsonString = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(URL);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "QXBwOkFwcA==");

        //        //GET Method  
        //        HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = client.PostAsync(URL, c).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            jsonString = response.Content.ReadAsStringAsync()
        //                                           .Result
        //                                           //.Replace("\\", "")
        //                                           //.Replace("\r\n", "'")
        //                                           .Trim(new char[1] { '"' });
        //        }

        //    }
        //    return jsonString;
        //}
        //public static string GetRequest(string URI, object parameterValues)
        //{
        //    string BaseURI = "http://localhost/MasterApi/api/"; //System.Configuration.ConfigurationManager.AppSettings["WebAPIURI"].ToString();

        //    string URL = BaseURI + URI;
        //    string jsonString = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(URL);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZURTZWN1cml0eTplX0RfQVBJLXVyaQ==");
        //        //GET Method  
        //        HttpResponseMessage response = client.GetAsync(URL).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            jsonString = response.Content.ReadAsStringAsync()
        //                                           .Result
        //                                           //.Replace("\\", "")
        //                                           .Trim(new char[1] { '"' });

        //        }
        //    }
        //    return jsonString;
        //}
        public async Task<string> AwaitPostRequest(string URI, string parameterValues)
        {
            string BaseURI = _objKeyList.Value.WebApiurl;
            string URL = BaseURI + URI;
            string jsonString = null;
            var jwt = GetJwt();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "QXBwOkFwcA==");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                //GET Method  
                HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, c).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   //.Replace("\r\n", "'")
                                                   .Trim(new char[1] { '"' });
                }

            }
            return await Task.FromResult(jsonString);
        }
        private string GetJwt()
        {
            try
            {
                _logger.LogInformation($"JWT token inti");

                var BaseURI = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("httpClient")["httpClientBaseURI"];
                //string BaseURI = "https://localhost:7150/api/";
                //string BaseURI = "http://cpdm.tasteofbollywood.in/api/";
                string URL = BaseURI + "auth?name=catcher&pwd=123";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Clear();
                var res2 = client.GetAsync(URL).Result;
                dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);
                _logger.LogInformation($"JWT token {jwt.access_token}");
                return jwt.access_token;

            }catch(Exception ex)
            {
                _logger.LogCritical($"JWT token ex::::{ex}");
                return null;
            }
        }
        public string PostRequestExternal(string URI, string parameterValues)
        {
            //string BaseURI = _objKeyList.Value.WebApiurl;
            //string URL = BaseURI + URI;
            string URL = URI;
            string jsonString = null;
            var jwt = GetJwt();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                //GET Method  
                HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, c).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   //.Replace("\r\n", "'")
                                                   .Trim(new char[1] { '"' });
                }

            }
            return jsonString;
        }

    }
}
