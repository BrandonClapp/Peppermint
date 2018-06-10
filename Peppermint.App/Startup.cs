using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peppermint.Blog;
using Peppermint.Commerce;
using Peppermint.Core;
using Peppermint.Forum;

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
    }
}
