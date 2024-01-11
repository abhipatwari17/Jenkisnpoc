using ApiCrudOperationsUsers.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ApiCrudOperationsUsers
{
    public class Apigateway
    {

        private string url = "https://localhost:7211/Users";

        private HttpClient httpClient = new HttpClient();
        public List<User> ListUsers()
        {
            List<User> users = new List<User>();
            if(url.Trim().Substring(0,5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<User>>(result);
                    if (datacol != null)
                    {
                        users = datacol;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at Api Endpoint, Error Info: " + result);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at Api Endpoint, Error Info: " + ex.Message);

            }
            finally
            { }
            return users;
        }




        public User CreateUser(User user)
        {
            url+= "/addUser";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            string json = JsonConvert.SerializeObject(user);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url,new StringContent(json, Encoding.UTF8,"application/json")).Result;
                if(!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at Api Endpoint, Error Info: " + result);
                }
                /*if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    *//*var data = JsonConvert.DeserializeObject<User>(result);
                    if (data != null)
                    {
                        user = data;
                    }*//*
                }*/
                /*else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at Api Endpoint, Error Info: " + result);

                }*/
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at Api Endpoint, Error Info: " + ex.Message);

            }
            
            return user;
        }
       
        public User GetUser(int id)
        {
            User user = new User();
            url += "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<User>(result);
                    if (data != null)
                    {
                        user = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at Api Endpoint, Error Info: " + result);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at Api Endpoint, Error Info: " + ex.Message);

            }
            finally
            { }
            return user;
        }


        public void UpdateUser(User user)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            int id = user.id;
            url += "/updateuser";
            string json = JsonConvert.SerializeObject(user);
            try
            {

                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at Api Endpoint, Error Info: " + result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at Api Endpoint, Error Info: " + ex.Message);

            }
            
            return;
        }


        /*public async Task UpdateUser(User user)
        {
            url += "/updateuser";
            try
            {
                if (url.Trim().StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                int id = user.id;
                string requestUrl = url + "/" + id;
                string json = JsonConvert.SerializeObject(user);

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (var response = await httpClient.PutAsync(requestUrl, content))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error occurred at API endpoint. Error Info: " + ex.Message);
            }
        }*/

        public void DeleteUser(int id)
         {
            url += "/deleteuser";
             if (url.Trim().Substring(0, 5).ToLower() == "https")
             {
                 ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
             }

             url += "/" + id;
             try
             {

                 HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                 if (!response.IsSuccessStatusCode)
                 {
                     string result = response.Content.ReadAsStringAsync().Result;
                     throw new Exception("Error occured at Api Endpoint, Error Info: " + result);
                 }

             }
             catch (Exception ex)
             {
                 throw new Exception("Error occured at Api Endpoint, Error Info: " + ex.Message);

             }
             finally
             { }
             return;
         }
        /*public async Task DeleteUser(int id)
        {
            try
            {
                if (url.Trim().StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                string requestUrl = url + "/" + id;

                HttpResponseMessage response = await httpClient.DeleteAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    throw new Exception("Error occurred at API Endpoint. Error Info: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred at API Endpoint. Error Info: " + ex.Message);
            }
        }*/


    }
}
