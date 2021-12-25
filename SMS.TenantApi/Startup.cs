using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SMS.TenantApi.BusinessModel.Config;
using SMS.TenantApi.BusinessService.Interfaces;
using SMS.TenantApi.BusinessService.Services;
using SMS.TenantApi.CrossCuttingLayer.Logging;
using SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces;
using SMS.TenantApi.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace SMS.TenantApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public AppSettingsModel Appsettings { get; }

        //Below Constructor is generated while creating the project....
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //builder.AddApplicationInsightsSettings(true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Appsettings = Configuration.GetSection("AppSettings").Get<AppSettingsModel>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCustomHeaders();
            services.AddSession();
            services.AddHealthChecks();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            var builder = services.AddMvcCore();
            builder.AddApiExplorer();
            //builder.AddJsonFormatters();
            builder.AddAuthorization();
            builder.AddFormatterMappings();
            //builder.AddJsonFormatters();
            builder.AddCors();

            //services.AddCustomHeaders();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            #region JWT Implementation
            //var key = Encoding.ASCII.GetBytes(Appsettings.JWTSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            #endregion

            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton(typeof(IServiceLogger), typeof(ServiceLogger));
            services.AddSingleton(typeof(IServices<>), typeof(Services<>));
            //services.AddSingleton(typeof(ICacheService), typeof(CacheService));
            //services.AddSingleton(typeof(MicrosoftCacheProvider), typeof(MicrosoftCacheProvider));
            //services.AddSingleton(typeof(RedisCacheProvider), typeof(RedisCacheProvider));

            //services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));

            //services.AddScoped<IUserContextAccessor, UserContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton<IUserService, UserService>();

            services.AddSingleton(Appsettings);
            services.AddScoped<Authorize>();

            //CORS Policy configurations
            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.SupportsCredentials = true;
            //services.AddCors(x => x.AddPolicy("EnableCors", policy)).BuildServiceProvider();

            services.AddSwaggerGen(); 
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", null
                    //new Info
                    //{
                    //    title = "SMS - Operation Api",
                    //    version = "v1",
                    //    description = "SMS - Operation Api",
                    //    termsOfService = "None"
                    //}
                );
                //enable the below line
                //options.OperationFilter<SwaggerAuthorizationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(filePath);
                //options.DescribeAllEnumsAsStrings();
            });

            services.Configure<Microsoft.AspNetCore.Builder.IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //    options.SerializerSettings.Formatting = Formatting.Indented;
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwagger(c =>
            {
                //c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Atgo2 :: Operation Api");
            });

            app.UseSwagger();

            //NEED TO ENABLE IT
            //app.UseAppMiddleware();
            
            //app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy().AddCustomHeader("X-Atgo-Http-Header", "Tripath"));

           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHealthChecks("/healthcheck");

            app.UseStaticFiles();
            app.UseCors("EnableCors");

            app.UseSession();
            app.UseAuthentication();
            app.UseMvc();

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}
