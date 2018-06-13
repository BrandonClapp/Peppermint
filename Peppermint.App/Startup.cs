using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peppermint.App.ViewModels;
using Peppermint.Blog;
using Peppermint.Commerce;
using Peppermint.Core;
using Peppermint.Forum;
using System.Linq;
using System.Reflection;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.App
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
            services.AddMvc();
            
            var connString = Configuration.GetConnectionString("Peppermint");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.Cookie.Name = "Peppermint";
                });



            // Register core Peppermint dependencies.
            services.AddPeppermint(connString);

            var assembly = Assembly.GetExecutingAssembly();
            RegisterViewModels<ViewModel>(assembly, services, LifeStyle.Transient);

            services.AddPeppermintBlog();
            services.AddPeppermintForum();
            services.AddPeppermintCommerce();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc();
        }

        private static void RegisterViewModels<TBase>(Assembly assembly, IServiceCollection services, LifeStyle lifeStyle)
            where TBase : ViewModel
        {
            var baseType = typeof(TBase);
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));

            foreach (var type in types)
            {
                if (lifeStyle == LifeStyle.Transient)
                {
                    services.AddTransient(type);
                }
                else if (lifeStyle == LifeStyle.Singleton)
                {
                    services.AddSingleton(type);
                }
            }
        }
    }
}
