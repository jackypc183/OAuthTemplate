using Newtonsoft.Json;
using RestSharp;
using Template.Models.OAuth;

namespace Template.Bll.OAuth
{
    public class OAuth<T>: IOAuth<T>
    {
        public string GetOAuthAuthorizeUrl(string url, OAuthUrlDto oauthUrlDto)
        {
            string redirectUrl = url +"?"+
                  $"response_type={oauthUrlDto.response_type}" +
                  $"&client_id={oauthUrlDto.client_id}" +
                  $"&redirect_uri={oauthUrlDto.redirect_uri}" +
                  $"&scope={oauthUrlDto.scope}" +
                  (oauthUrlDto.state != null ? $"&state={oauthUrlDto.state}" : "") +
                  (oauthUrlDto.nonce != null ? $"&nonce={oauthUrlDto.nonce}" : "");
            return redirectUrl;
        }

        public async Task<T> GetOAuthToken(string url, OAuthTokenDto oAuthTokenDto)
        {
            var client = new RestClient();
            var request = new RestRequest($"{url}", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", $"{oAuthTokenDto.grant_type}");
            request.AddParameter("client_id", $"{oAuthTokenDto.client_id}");
            request.AddParameter("client_secret", $"{oAuthTokenDto.client_secret}");
            request.AddParameter("code", $"{oAuthTokenDto.code}");
            request.AddParameter("redirect_uri", $"{oAuthTokenDto.redirect_uri}");
            var response = await client.ExecuteAsync(request);
            var context = JsonConvert.DeserializeObject<T>(response.Content);
            return context;
        }
    }
}
