using Geo.Rest.Business.Automapper;
using Geo.Rest.Business.Services;
using Geo.Rest.Business.Services.Interfaces;
using Geo.Rest.Data.Contexts;
using Geo.Rest.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;

namespace Geo.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProfileAssembly)));

            services.AddDbContext<GeoContext>(ServiceLifetime.Transient);

            services.TryAddTransient<IGeoService, GeoService>();

            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                options.HttpsPort = 443;
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "geo-service.rest",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Brecht Baekelandt",
                        Email = "brecht@geo-service.rest",
                        Url = new Uri("https://geo-service.rest")
                    }
                });

                c.DescribeAllParametersInCamelCase();

                var xmlDocumentFile = typeof(DomainAssembly).Assembly.Location.Replace("dll", "xml");

                if (File.Exists(xmlDocumentFile))
                {
                    c.IncludeXmlComments(xmlDocumentFile);
                }
            });

            services.AddResponseCaching();

            services
                .AddMvcCore()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "geo-service.rest v1"));

            app.UseStaticFiles();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            //app.UserCors();

            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                {
                    Public = true
                };               

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "export", pattern: "export/", new { controller = "Home", action = "Export" });
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/", new { controller = "Home", action = "Index" });
                endpoints.MapControllers();
            });
        }
    }
}
