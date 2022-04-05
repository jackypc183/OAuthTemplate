using Template.Models;
using Template.Models.Line;
using Template.Models.OAuth;

namespace Template.Service.OAuth
{
    public class OAuthService<T> : IOAuthService<T>
    {
        private IOAuth<T> _oAuth;
        public OAuthService(IOAuth<T> oAuth)
        {
            _oAuth = oAuth;
        }
        public string GetOAuthAuthorizeUrl(string url, OAuthUrlDto oauthUrlDto)
        {
            return _oAuth.GetOAuthAuthorizeUrl(url, oauthUrlDto);
        }

        public async Task<T> GetOAuthToken(string url, OAuthTokenDto oAuthTokenDto)
        {
            return await _oAuth.GetOAuthToken(url, oAuthTokenDto);
        }
    }
}
