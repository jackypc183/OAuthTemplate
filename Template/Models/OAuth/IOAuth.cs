namespace Template.Models.OAuth
{
    public interface IOAuth<T>
    {
        public string GetOAuthAuthorizeUrl(string url, OAuthUrlDto oauthUrlDto);
        public Task<T> GetOAuthToken(string url, OAuthTokenDto oAuthTokenDto);
    }
}
