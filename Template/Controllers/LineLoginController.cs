using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;
using Template.Models;
using Template.Models.Line;
using Template.Models.OAuth;
using Template.Service.OAuth;
using Template.Service.Users;

namespace Template.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LineLoginController : ControllerBase
    {
        IUserService _userService;
        IOAuthService<LineTokenResultDto> _lineOAuthService;
        IConfiguration _configuration;
        OAuthUrlDto _oAuthUrlDto;
        OAuthTokenDto _oAuthTokenDto;
        LineLogin oAuthParameter;
        JwtHelpers _jwt;
        public LineLoginController(
            IUserService userService,
            IOAuthService<LineTokenResultDto> lineOAuthService,
            IConfiguration configuration,
            JwtHelpers jwt)
        {
            _userService = userService;
            _lineOAuthService = lineOAuthService;
            _configuration = configuration;
            oAuthParameter  = _configuration.GetSection("LineLogin").Get<LineLogin>();
            _jwt = jwt;

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
        public async Task<ApiResult<string>> Token([FromBody] TokenReqDto tokenReqDto)
        {
            string code = tokenReqDto.code;
            string state = tokenReqDto.state;
            Console.WriteLine($"{code}{state}");
            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.State = false;
            _oAuthTokenDto.code = $"{code}";
            var context = await _lineOAuthService.GetOAuthToken(oAuthParameter.token, _oAuthTokenDto);
            var access_token = context.access_token;
            Console.WriteLine(access_token);
            if(access_token != null)
            {
                var line_porfile = await LineProfile(access_token);
                var user = _userService.GetList().Where(p => p.lineUserId == line_porfile.Result.userId).FirstOrDefault();
                if (user is null)
                {
                    UserDto userDto = new UserDto()
                    {
                        id = Guid.NewGuid(),
                        account = line_porfile.Result.userId,
                        password = Guid.NewGuid().ToString(),
                        lineToken = access_token,
                        lineUserId = line_porfile.Result.userId,
                        userName = line_porfile.Result.displayName
                    };
                    _userService.InsterUser(userDto);
                }
                else
                {
                    _userService.UpdateUser(user);
                }

                var jwt_accessToken = _jwt.GenerateToken(line_porfile.Result.userId);
                apiResult.Result = jwt_accessToken;
                apiResult.State = true;

            }
            return apiResult;
        }

        [Route("LineProfile")]
        [HttpGet]
        public async Task<ApiResult<LineProfileResultDto>> LineProfile(string token)
        {
            var client = new RestClient();
            var request = new RestRequest(oAuthParameter.profile, Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);
            ApiResult<LineProfileResultDto> apiResult = new ApiResult<LineProfileResultDto>();
            if (response.IsSuccessful)
            {
                var context = JsonConvert.DeserializeObject<LineProfileResultDto>(response.Content);
                apiResult.State = true;
                apiResult.Result = context;
            }
            else
            {
                apiResult.State = false;
            }
            return apiResult;
        }
    }



    public class LineLogin
    {
        public string authorize { get; set; }
        public string token { get; set; }
        public string profile { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
    }

}
