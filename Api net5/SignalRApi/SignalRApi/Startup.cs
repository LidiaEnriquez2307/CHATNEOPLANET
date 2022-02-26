using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SignalRApi.Data;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Data.Repositorios;
using SignalRApi.SingnalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi
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
            var mySQLConnectionConfig = new MySQLConfiguration(Configuration.GetConnectionString("MySqlConnection"));
            services.AddSingleton(mySQLConnectionConfig);//Coneccion a la BD
            services.AddSingleton<InterfaceCuenta,RepoCuenta>(); //Tabla Cuenta 
            services.AddSingleton<InterfaceSala, RepoSala>(); //Tabla Sala
            services.AddSingleton<InterfaceMensaje, RepoMensaje>(); //Tabla Mensaje
            services.AddSingleton<InterfaceCuentaSala, RepoCuentaSala>(); //Tabla CUENTA_SALA
            services.AddSingleton<InterfaceTipoSala, RepoTipoSala>(); //Tabla TIPO_SALA
            services.AddSingleton<InterfaceUMNV, RepoUMNV>(); //Tabla ULTIMO_MENSAJE_NO_VISIBLE

            services.AddControllers();
            services.AddSignalR(); //SignalR
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignalRApi v1"));
            }
            if(env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/mysite/swagger/v1/swagger.json", "SignalRApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
