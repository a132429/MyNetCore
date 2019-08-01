using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yq.Application;
using Yq.Application.RoleApp;
using Yq.Application.UserApp;
using Yq.Domain;
using Yq.Domain.IRepository;
using Yq.Domain.Repository;
using Yq.EntityFrameworkCore;
using Yq.WebApi.Model;
using Yq.WebApi.SwaggerHelp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.CompilerServices;

namespace Yq.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化映射关系
            YqMapper.Initialize();
        }

        public IConfiguration Configuration { get; }


        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");
            var conDataType = Configuration.GetConnectionString("dataBaseType");
            //添加数据上下文
            services.AddDbContext<WdDbContext>(options => options.UseMySql(sqlConnectionString));//UseSqlServer

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:8000");
                });
            });
           
            #region JWT
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options => {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => {
                //主要是jwt  token参数设置
                o.TokenValidationParameters = new  TokenValidationParameters
                {
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //颁发给谁
                    ValidAudience = jwtSettings.Audience,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    //ValidateIssuerSigningKey=true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime=true,
                    ////允许的服务器时间偏移量
                    //ClockSkew=TimeSpan.Zero

                };
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .AddRequirements(new Model.Jwt.ValidJtiRequirement()) // 添加上面的验证要求
                    .Build());
            });

            #endregion

            #region CSRedisClient
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,password=r213456,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240,prefix=");
            //提示：CSRedis.CSRedisClient 单例模式够用了，强烈建议使用 RedisHelper 静态类
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            #endregion
            //依赖注入
            services.AddSingleton(new DapperContext(sqlConnectionString, conDataType));
            services.ResolveAllTypes(new string[] { "Yq.Application", "Yq.Domain" });
            services.AddScoped<IServiceProvider, ServiceProvider>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            services.AddScoped<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, Model.Jwt.ValidJtiHandler>();
            #region Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "TestSwagger API"
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Yq.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
                c.DocumentFilter<SwaggerDocTag>();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            SeedData.Initialize(serviceProvider);
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            //配置authorrize
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yq.WebApi V1");
            });
            #endregion
        }
    }
}
