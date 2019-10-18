using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace MicrosoftGraph.Planner.Console.Service
{
    public class GraphService
    {
        private readonly string _endpoint;
        private readonly string _apiVersion;
        private readonly string _applicationId;
        private readonly string _clientSecret;
        private readonly string _user;
        private readonly string _password;
        private readonly string _authorityUri;

        private string _baseUri;

        public string BaseUri
        {
            get => _baseUri;
            set => _baseUri = _endpoint + "/" + _apiVersion + value;
        }

        public GraphService()
        {
            _endpoint = ConfigurationManager.AppSettings["ApiUrl"];
            _apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
            _applicationId = ConfigurationManager.AppSettings["ApplicationId"];
            _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            _authorityUri = string.Format(ConfigurationManager.AppSettings["AuthorityUri"], ConfigurationManager.AppSettings["Tenant"]);
            _user = ConfigurationManager.AppSettings["User"];
            _password = ConfigurationManager.AppSettings["Password"];
            _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        }

        private async Task<string> GetTokenForUserCredentialsAsync()
        {
            var authContext = new AuthenticationContext(_authorityUri);
            var credentials = new UserPasswordCredential(_user, _password);
            var authResult = await authContext.AcquireTokenAsync(_endpoint, _applicationId, credentials);
            return authResult.AccessToken;
        }

        protected async Task<T> GetResponse<T>(string url = null)
        {
            string jsonresult = null;

            if (string.IsNullOrEmpty(url))
                url = BaseUri;

            try
            {
                using (var client = new HttpClient())
                {
                    const string accept = "application/json";
                    client.DefaultRequestHeaders.Add("Accept", accept);
                    var accessToken = await GetTokenForUserCredentialsAsync();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    using (var response = await client.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            jsonresult = await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            throw new Exception("Error getting data: " + response.StatusCode.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return JsonConvert.DeserializeObject<T>(jsonresult);
        }
    }
}
