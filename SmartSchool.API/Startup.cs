using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.API.Data;
using System;
using System.IO;
using System.Reflection;

namespace SmartSchool.API
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
            services.AddDbContext<SmartContext>(context => context.UseSqlite(Configuration.GetConnectionString("Default")));

            services.AddScoped<IRepository, Repository>();// Inversão de controle

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Mapeamento

            services.AddControllers()
                .AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);//Para o looping das classes internas, força ele a fazer a primeira camada 

            services.AddVersionedApiExplorer(options => // Para fazer a versão do código 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
                .AddApiVersioning(options => // para definir a versão padrão
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>{ //Configuração do swagger
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchool API",
                        Version = description.ApiVersion.ToString(),
                        TermsOfService = new Uri("http://Termos.com"),
                        Description = "System for school",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "SmartSchool Licence",
                            Url = new Uri("http://mit.com")
                        },
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Sandro Gonçalves",
                            Email = "gonsandroo@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/sandro-gon%C3%A7alves-825b6b167/")
                        }
                    }
                  );
                }

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Configuração para escrever os comentários no swagger 
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI( options =>
            {
                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
                }
               
                options.RoutePrefix = "";
            });
        }
    }
}
