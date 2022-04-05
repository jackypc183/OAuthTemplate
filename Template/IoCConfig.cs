using Microsoft.EntityFrameworkCore;
using Template.Models.Line;

namespace Template
{
    public class IoCConfig
    {
        /// <summary>
        /// 設定註冊服務
        /// </summary>
        /// <param name="Configuration">設定檔</param>
        /// <param name="services">服務集合</param>
        /// <returns></returns>
        public static IServiceCollection Configure(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddScoped<DbContext, TemplateDBContext>();
            services.AddScoped<Template.Repository.IUnitOfWork, Template.Repository.UnitOfWork>();
            services.AddScoped<Template.Dal.IUserRepository, Template.Dal.UserRepository>();
            services.AddScoped<Template.Repository.Dal.LineNotifyToken.ILineNotifyTokenRepository, Template.Repository.Dal.LineNotifyToken.LineNotifyTokenRepository>();
            services.AddScoped<Template.Service.Users.IUserService, Template.Service.Users.UserService>();
            services.AddScoped(typeof(Template.Service.OAuth.IOAuthService<>), typeof(Template.Service.OAuth.OAuthService<>));
            services.AddScoped(typeof(Template.Models.OAuth.IOAuth<>), typeof(Template.Bll.OAuth.OAuth<>));

            return services;
        }
    }
}
