using Backend_EctoTec.Core.Interfaces.Repositories;
using Backend_EctoTec.Core.Interfaces.Services;
using Backend_EctoTec.Repositorios;
using Backend_EctoTec.Services;
using Backend_EctoTec_API.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Backend_EctoTec_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                var frontendURL = Configuration["frontend_url"];
                options.AddPolicy("Policy", builder =>
                {
                    builder.WithOrigins(frontendURL)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            services.AddTransient<IServiceGreenLeaves, ServiceGreenLeaves>();
            services.AddTransient<IRepositoryGreenLeaves, RepositoryGreenLeaves>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<SendEmailService>();
            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foo API V1");
                });
            }

            app.UseRouting();
            app.UseCors("Policy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Foo {groupName}",
                    Version = groupName,
                    Description = "Foo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Foo Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }
    }
}
