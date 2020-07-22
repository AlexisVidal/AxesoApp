using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_SL.GraphQL;
using Axeso_SL.Interfaces;
using Axeso_SL.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Axeso_SL
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
            
            services.AddScoped<IRepository, Repository>();

            services.AddDbContext<GraphQLDemoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            
            //GraphQL configuration
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AxesoSchema>();
            //services.AddScoped<CulturaSchema>();
            //services.AddScoped<UsuarioSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<AxesoSchema>();
            //app.UseGraphQL<UsuarioSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());


            app.UseMvc();
        }
    }
}
