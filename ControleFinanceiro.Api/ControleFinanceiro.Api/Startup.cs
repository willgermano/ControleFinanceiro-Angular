using ControleFinanceiro.Data;
using ControleFinanceiro.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api
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
            //Configurar a classe de contexto
            services.AddDbContext<Context>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoDB")));

            //Configura Identity
            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Context>();

            //Responsavel por fazer a comunicacao entre o Angular e Api (Front e Back)
            services.AddCors();

            //Configurar onde está os arquivos do Angular
            services.AddSpaStaticFiles(diretorio =>
            {
                diretorio.RootPath = "ControleFinanceiro-UI";
            });

            services.AddControllers()
                .AddJsonOptions(opcoes =>
                {
                    opcoes.JsonSerializerOptions.IgnoreNullValues = true; //Api ignorar valores nulos
                })
                .AddNewtonsoftJson(opcoes =>
                {
                    opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; //Api ignorar referencias seculares
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControleFinanceiro.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleFinanceiro.Api v1"));
            }

            //Configurar o Cors
            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Configurando o Spa
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI"); //Onde está localizado o Front em Angular

                if (env.IsDevelopment())
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/"); 
            });
        }
    }
}
