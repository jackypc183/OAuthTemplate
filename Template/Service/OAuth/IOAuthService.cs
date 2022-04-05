using Template.Models.OAuth;

namespace Template.Service.OAuth
{
    public interface IOAuthService<T>
    {
        public string GetOAuthAuthorizeUrl(string url, OAuthUrlDto oauthUrlDto);
        public Task<T> GetOAuthToken(string url, OAuthTokenDto oAuthTokenDto);
    }
}
