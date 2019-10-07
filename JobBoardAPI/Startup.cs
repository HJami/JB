using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using JB.services;
using JB.models;
using Microsoft.Extensions.Configuration;

namespace JB
{
    public class Startup
    {
        
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<JobBoardDatabaseSettings>(
                Configuration.GetSection(nameof(JobBoardDatabaseSettings)));

            services.AddSingleton<IJobBoardDatabaseSettings>(sp =>
               sp.GetRequiredService<IOptions<JobBoardDatabaseSettings>>().Value);

            services.Configure<RedisSettings>(
                Configuration.GetSection(nameof(RedisSettings)));

            services.AddSingleton<IRedisSettings>(sp =>
               sp.GetRequiredService<IOptions<RedisSettings>>().Value);

            services.AddSingleton<RedisService>();

            services.Add(new ServiceDescriptor
                           (typeof(IJobService), typeof(JobService), ServiceLifetime.Singleton));

            services.AddSingleton<JobBoardStoreService>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
