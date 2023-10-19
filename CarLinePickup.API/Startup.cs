using CarLinePickup.API.Middleware;
using CarLinePickup.API.Models.Request;
using CarLinePickup.API.Validators.Address;
using CarLinePickup.Composition;
using CarLinePickup.Options;
using CarLinePickup.Options.Extensions;
//using CarLinePickup.Requirement;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string allowedOrigins = "_allowedOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            var origins = Configuration.GetValue<string>("AllowedOrigins");

            if (!string.IsNullOrWhiteSpace(origins))
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(allowedOrigins,
                   corsBuilder => corsBuilder

                       .WithOrigins(origins)
                       .SetIsOriginAllowedToAllowWildcardSubdomains()
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                   );
                });
            }
            //Add the service required for using the built in options pattern
            services.AddOptions();
            services.AddFeatureManagement();

            //TODO: what do we use for authorization to this service from the API?

            services.AddMvc()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     options.JsonSerializerOptions.IgnoreNullValues = true;
                 })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //Add the validators to the DI container that need to be manually executed
            services.AddTransient<IValidator<AddressRequestUpdate>, UpdateAddressValidator>();
            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddDomainOptions(Configuration);

            services.AddContainerOptions(Configuration);

            var containerOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ContainerOptions>>();

            // Install the container, using the composite root configuration
            services.AddDependencyGraph(containerOptions);
            services.AddAzureAppConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(allowedOrigins);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
