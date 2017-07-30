using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FirstApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace FirstApp {
    public class Startup {
        public Startup(IHostingEnvironment env) { }

        public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<DatabaseContext>();
            services.AddMvc();
		}
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole();

			app.UseCookieAuthentication(new CookieAuthenticationOptions {
				AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
				LoginPath = new PathString("/login"),
				AccessDeniedPath = new PathString("/login"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true
			});

			app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
			app.UseMvc();
        }
    }
}
