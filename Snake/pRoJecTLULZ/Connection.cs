using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace pRoJecTLULZ
{
    public class Connection
    {
        private static readonly HttpClient client = new HttpClient();

        private static string? sessionToken;

        //singelton instance
        private static Connection? instance;
        public static Connection getInstance()
        {
            if(instance == null)
            {
                instance = new Connection();
            }

            return instance;
        }

        private Connection()
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", "key");
        }

        public async Task<string> Login(string username, string password)
        {
            var jsonObject = new JsonObject();
            jsonObject.Add("username", username);
            jsonObject.Add("password", password);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"); ;

            var response = await client.PostAsync("http://localhost:5000/api/login", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Count(t => t == '-') == 4) {
                sessionToken = responseString;
            }

            return responseString;
        }

        public async Task<string> Register(string username, string password)
        {
            var jsonObject = new JsonObject();
            jsonObject.Add("username", username);
            jsonObject.Add("password", password);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"); 

            var response = await client.PostAsync("http://localhost:5000/api/register", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<string> AddHighscore(int score)
        {
            var jsonObject = new JsonObject();
            jsonObject.Add("sessionToken", sessionToken);
            jsonObject.Add("score", score);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"); 

            var response = await client.PostAsync("http://localhost:5000/api/highscore", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<string> GetHighscores()
        {
            var response = await client.GetAsync("http://localhost:5000/api/highscore");
            var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("fdsfsdfsd");
            return responseString;
        }
    }
}
