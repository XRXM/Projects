using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public static class AccountService 
    {

        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private static readonly IRestClient client = new RestClient();
        
        
        public static Accounts AccountBalance(int id)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"account/{id}");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<Accounts> response = client.Get<Accounts>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authorization is required for this option. Please log in.");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception("You do not have permission to perform the requested action");
                }

                throw new Exception($"Error occurred - received non-success response: {response.StatusCode} ({(int)response.StatusCode})");
            }

            return response.Data;
        }
    }
}
