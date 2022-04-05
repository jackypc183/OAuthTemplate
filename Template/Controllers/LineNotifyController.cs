using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;
using Template.Models;
using Template.Models.Line;
using Template.Models.LineNotifyToken;
using Template.Models.OAuth;
using Template.Repository.Dal.LineNotifyToken;
using Template.Service.OAuth;

namespace Template.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Users")]
    public class LineNotifyController : ControllerBase
    {
        IOAuthService<LineTokenResultDto> _lineOAuthService;
        IConfiguration _configuration;
        OAuthUrlDto _oAuthUrlDto;
        OAuthTokenDto _oAuthTokenDto;
        LineNotify oAuthParameter;
        ILineNotifyTokenRepository _lineNotifyTokenRepository;
        public LineNotifyController(
            IOAuthService<LineTokenResultDto> lineOAuthService,
            IConfiguration configuration,
            ILineNotifyTokenRepository lineNotifyTokenRepository)
        {
            _lineOAuthService = lineOAuthService;
            _configuration = configuration;
            oAuthParameter = _configuration.GetSection("LineNotify").Get<LineNotify>();
            _lineNotifyTokenRepository = lineNotifyTokenRepository;

            _oAuthUrlDto = new OAuthUrlDto()
            {
                response_type = "code",
                client_id = oAuthParameter.client_id,
                redirect_uri = oAuthParameter.redirect_uri,
                scope = oAuthParameter.scope,
                state = "123"
            };
            _oAuthTokenDto = new OAuthTokenDto()
            {
                grant_type = "authorization_code",
                client_id = oAuthParameter.client_id,
                client_secret = oAuthParameter.client_secret,
                redirect_uri = oAuthParameter.redirect_uri
            };
        }
        [Route("AuthorizeUrl")]
        [HttpGet]
        public string AuthorizeUrl()
        {
            string redirectUrl = _lineOAuthService.GetOAuthAuthorizeUrl(oAuthParameter.authorize, _oAuthUrlDto);
            return redirectUrl;
        }

        [Route("Token")]
        [HttpPost]
        public async Task<ApiResult<LineProfileResultDto>> Token(TokenReqDto tokenReqDto)
        {
            ApiResult<LineProfileResultDto> apiResult = new ApiResult<LineProfileResultDto>();
            apiResult.State = false;
            var lineUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if(lineUserId != null)
            {
                string code = tokenReqDto.code;
                string state = tokenReqDto.state;
                Console.WriteLine($"{code}{state}");
                _oAuthTokenDto.code = $"{code}";
                var context = await _lineOAuthService.GetOAuthToken(oAuthParameter.token, _oAuthTokenDto);
                var access_token = context.access_token;
                Console.WriteLine(access_token);
                if (access_token != null)
                {
                    var lineNotifyToken = _lineNotifyTokenRepository.GetList().Where(p => p.lineNotifyToken == access_token).FirstOrDefault();
                    if (lineNotifyToken is null)
                    {
                        LineNotifyTokenDto dto = new LineNotifyTokenDto()
                        {
                            id = Guid.NewGuid(),
                            lineUserId = lineUserId,
                            lineNotifyToken = access_token
                        };
                        bool InsterStates = _lineNotifyTokenRepository.Inster(dto);
                        if (InsterStates)
                        {
                            apiResult.State = true;
                        }
                    }

                }
            }
            else
            {
                apiResult.Message = "Token抓不到userid";
            }
            return apiResult;
        }

        [Route("SendAllNotify")]
        [HttpPost]
        public async Task<ApiResult<string>> SendAllNotify([FromBody]string message)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.State = false;
            var token = _lineNotifyTokenRepository.GetList().Select(p=>p.lineNotifyToken).ToList();
            foreach(var t in token)
            {
                var client = new RestClient();
                string url = oAuthParameter.send_msg_uri;
                var request = new RestRequest($"{url}", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", $"Bearer {t}");
                request.AddParameter("message", $"{message}");
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    apiResult.State = true;
                }
            }
            return apiResult;
        }

        [Route("RevokeLineNotify")]
        [HttpGet]
        public async Task<ApiResult<string>> RevokeLineNotify()
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.State = false;
            var lineUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var dto = _lineNotifyTokenRepository.GetList().Where(x=>x.lineUserId == lineUserId).ToList();
            foreach (var t in dto)
            {
                var client = new RestClient();
                string url = oAuthParameter.revoke_uri;
                var request = new RestRequest($"{url}", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", $"Bearer {t.lineNotifyToken}");
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    apiResult.State = true;
                    _lineNotifyTokenRepository.Delete(t.id);
                }
            }
            return apiResult;
        }
    }
    public class LineNotify
    {
        public string authorize { get; set; }
        public string token { get; set; }
        public string profile { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
        public string send_msg_uri { get; set; }
        public string revoke_uri { get; set; }
    }
}
