using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    //Get configuration
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;


        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AVp0spoGvxkPv1OQOlLYW8gvMGaFjEgFGKyyoq1iCZ2tjtdlqbFpdBmlJJJQQcruekygMzUJycEd1WzT";
            clientSecret = "EP0xAtw3_DcLGAoC81vbkZnrh9B38AbkUrnAexaczMe90GZb6XC4vO-NAcAKgO_YqjuXf23C9SQJ3pai";
        }

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }

    }
}