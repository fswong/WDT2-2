using Assignment2WebAPI.REST;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment2.Helpers
{
    public static class HttpClientHelper
    {
        #region params
        static string _baseUrl = "http://localhost:62530/api/";
        #endregion

        #region methods

        /// <summary>
        /// makes a post to a specified endpoint
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static T Post<T>(FormUrlEncodedContent content, string endpoint) where T : IHttpResponse
        {
            try
            {
                var httpClient = InitialiseHttpClient();
                var responseMessage = Task.Run(() => httpClient.PostAsync(endpoint, content)).Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception($"Post to {endpoint} did not return a success " + responseMessage.StatusCode);
                }

                var response = JsonConvert.DeserializeObject<T>(responseMessage.Content.ReadAsStringAsync().Result);
                return (T)response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// calls a get from a specified endpoint
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static T Get<T>(string endpoint) where T : IHttpResponse
        {
            try
            {
                // instantiates the httpclient and furnishes its headers
                var httpClient = InitialiseHttpClient();

                // previous .net versions had issues with tls, this might not be needed anymore
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // make the call (sync)
                var responseMessage = httpClient.GetAsync(endpoint).Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception($"Call to {endpoint} did not return a success " + responseMessage.IsSuccessStatusCode);
                }

                // deserialises the message
                var responseString = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                var response = JsonConvert.DeserializeObject<T>(responseString);

                return (T)response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// returns an API collection
        /// </summary>
        /// <typeparam name="IRESTObject"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RESTCollection<IRESTObject> GetCollection<IRESTObject>(string endpoint) 
        {
            try
            {
                // instantiates the httpclient and furnishes its headers
                var httpClient = InitialiseHttpClient();

                // previous .net versions had issues with tls, this might not be needed anymore
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // make the call (sync)
                var responseMessage = httpClient.GetAsync(endpoint).Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception($"Call to {endpoint} did not return a success " + responseMessage.IsSuccessStatusCode);
                }

                // deserialises the message
                var responseString = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                var response = JsonConvert.DeserializeObject<List<IRESTObject>>(responseString);
                var collection = new RESTCollection<IRESTObject> { data = response };

                return collection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// generic test method that hits the panviva's echo api
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static HttpStatusCode Test(string endpoint)
        {
            try
            {
                // instantiates the httpclient and furnishes its headers
                var httpClient = InitialiseHttpClient();

                // previous .net versions had issues with tls, this might not be needed anymore
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // make the call (sync)
                var responseMessage = httpClient.GetAsync(endpoint).Result;

                return responseMessage.StatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region misc

        /// <summary>
        /// initialiser
        /// furnishes the httpclient with the authentication
        /// </summary>
        /// <returns></returns>
        private static HttpClient InitialiseHttpClient()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient httpClient = new HttpClient(handler);
                httpClient.BaseAddress = new Uri(_baseUrl); ;
                return httpClient;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
