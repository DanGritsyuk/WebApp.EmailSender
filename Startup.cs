using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppEmailSender.Data;
using WebAppEmailSender.EmailService;
using WebAppEmailSender.Models;

namespace WebAppEmailSender
{
    public class Startup
    {

        private readonly IConfigurationRoot _configString;

        public Startup(IHostEnvironment hostEnv)
        {
            _configString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").AddJsonFile("smptsettings.json").Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmsDbContext>(options => options.UseSqlServer(_configString.GetConnectionString("DevConnection")));
            services.AddScoped<DbEmailSenderInfo>();
            services.AddScoped<MailViewModel>();

            var emailConfig = _configString.GetSection("EmailConfiguration").Get<SmptConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddControllers();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddMvc().WithRazorPagesRoot("/Views");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
