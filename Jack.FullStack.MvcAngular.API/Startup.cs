using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Jack.DataScience.Common;
using Autofac;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Autofac.Extensions.DependencyInjection;
using Jack.FullStack.MvcAngular.API.Authorization;
using Jack.FullStack.MvcAngular.API.ErrorHandling;
using Jack.FullStack.MvcAngular.API.Dtos;

namespace Jack.FullStack.MvcAngular.API
{
    public class Startup
    {
        public static IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public const string CorsPolicy = "Cors";

        public const string ArangoConnectionId = "_system";

        public const string SwaggerApiName = "medigraph-api";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine($"Current Environment is '{env}'");

            AutoFacContainer autoFacContainer = new AutoFacContainer(env);

            ContainerBuilder builder = autoFacContainer.ContainerBuilder;

            builder.RegisterType<RoleJwtEncoder>();

            services.AddCors(options =>
                    options.AddPolicy(
                        CorsPolicy,
                        corsBuilder =>
                            corsBuilder
                                .SetIsOriginAllowed(url => true)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                        )
                );

            services.AddMvc().AddJsonOptions(json =>
            {
                json.SerializerSettings.Error = OnJsonError;
                json.SerializerSettings.ContractResolver = new DefaultContractResolver();
                json.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerGen(
                setup =>
                    setup.SwaggerDoc(SwaggerApiName,
                    new Info
                    {
                        Version = "1",
                        Title = "MediGraph API Server",
                        Description = "MediGraph API",
                        TermsOfService = "N/A"
                    })
                );

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void OnJsonError(object source, ErrorEventArgs error)
        {
            Debugger.Break();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(CorsPolicy);
            }
            else
            {
                app.UseHsts();
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(setup => setup.SwaggerEndpoint($"/swagger/{SwaggerApiName}/swagger.json", "MediGraph API"));
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spaFallback", new { controller = "Home", action = "Spa" });
            });
        }
    }
}
