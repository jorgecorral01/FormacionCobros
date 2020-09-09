using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charge.Activity.Service.Controllers;
using Charge.Activity.Service.Factories;
using Charge.Activity.Service.swagger;
using Charge.Repository.Service.Repository.Entity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Charge.Activity.Service {
    public class Startup {
        private const string ApiDescription = "Charges Activities Service";
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            ConfigureMvc(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ChargesContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlFormacion")));
            services.AddSingleton(new RepositoriesFactory());
            ConfigureSwagger(services);
        }

        private void ConfigureSwagger(IServiceCollection services) {
            services.AddSwaggerGen(swaggerGenOptions => {
                for(var v = ApiVersioning.Min;v <= ApiVersioning.Current;v += 1) {
                    swaggerGenOptions.SwaggerDoc($"v{v}", new Info { Title = ApiDescription, Version = $"v{v}" });
                }

                swaggerGenOptions.CustomSchemaIds(x => x.FullName);

                swaggerGenOptions.AddSecurityDefinition("oauth2", new ApiKeyScheme {
                    Description =
                        "Standard Authorization header using the Bearer scheme. Remember the to write the \"bearer\" keyword. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                swaggerGenOptions.OperationFilter<SecurityRequirementsOperationFilter>();
                swaggerGenOptions.OperationFilter<SwaggerFileUploadOperation>();
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseCors("MyPolicy")
                .UseSwagger(c => {

                })
                .UseSwaggerUI(
                    options => {
                        foreach(var description in provider.ApiVersionDescriptions) {
                            options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                            options.RoutePrefix = string.Empty;
                        }
                    })
                .UseAuthentication()
                .UseMvc();
        }

        private void ConfigureMvc(IServiceCollection services) {
            services
                .AddMvcCore(config => {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                    //config.Filters.Add(new RequestBodyInsightsFilter(StatusCodes.Status400BadRequest));
                })
                .AddAuthorization()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddXmlSerializerFormatters()
                .AddCors()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddVersionedApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options => {
                options.UseApiBehavior = false;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                ChargeActivityController.Convention(options);
            });
        }
    }
}
