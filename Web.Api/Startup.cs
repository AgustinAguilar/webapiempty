using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Api.ViewModels.ClassesBase;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Web.Api.Model;
using Web.Api.Exceptions;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IWebHostEnvironment webHostingEnvironment)
        {
            Configuration = configuration;
            WebHostingEnvironment = webHostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostingEnvironment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseViewModel>());
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region Cors

            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
            }));

            #endregion

            ConfigureAuth(services);

            #region Services

            GetTypes("Services", "Service").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion

            #region Repositories

            GetTypes("Repositories", "Repository").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion

            #region DbContext
            var conn = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DomainContext>(options => options.UseSqlServer(conn));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(conf => { conf.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly; }, Assembly.GetExecutingAssembly());
            #endregion

            services.AddHttpClient();

            #region Swagger
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            var environmentName = WebHostingEnvironment.EnvironmentName;
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"Web Api ", Version = $"{version} - {environmentName}" });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRulesScoped();
                c.AddSecurityDefinition("WebApiKey", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "WebApiKey",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the WebApiKey scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "WebApiKey"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Api");
            });

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseExceptionHandlerMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = signingKey,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                   ClockSkew = TimeSpan.Zero
               };
           });
        }

        private IDictionary<Type, Type> GetTypes(string nameSpace, string endWith)
        {
            var res = new Dictionary<Type, Type>();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var assemblyTypes = thisAssembly.GetTypes();
            foreach (var typeImplementation in assemblyTypes
                     .Where(p => p.Name.EndsWith(endWith))
                     .Where(p => !p.Name.Contains("Generic"))
                     .Where(t => string.Equals(t.Namespace, thisAssembly.GetName().Name + "." + nameSpace, StringComparison.Ordinal)))
                res.Add(assemblyTypes.FirstOrDefault(p => p.Name == "I" + typeImplementation.Name), typeImplementation);

            return res;
        }
    }
}
