using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGK2V.Contexto;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APIGK2V
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
            services.AddSingleton<IContextoMongo,mongoContext>();
            services.AddSingleton<IRepositorioBase<EntidadeBase>,RepositorioBase<EntidadeBase>>();
            services.AddSingleton<IPessoaRepositorio,PessoaRepositorio>();
            services.AddTransient<ICupRepositorio,CupRepositorio>();
            services.AddTransient<IMatchRepositorio,MatchRepositorio>();
            services.AddTransient<IPlayerRepositorio,PlayerRepositorio>();
            services.AddTransient<IUsuarioRepositorio,UsuarioRepositorio>();
            services.AddTransient<ITemporadaRepositorio,TemporadaRepositorio>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
