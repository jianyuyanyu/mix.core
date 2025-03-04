using Mix.Database.Entities.Account;
using System.Reflection;

namespace Mix.Account
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
            MixFileHelper.CopyFolder(MixFolders.MixCoreConfigurationFolder, MixFolders.MixContentFolder);
            services.AddMixServices(Assembly.GetExecutingAssembly(), Configuration);
            services.AddMixCors();

            // Must app Auth config after Add mixservice to init App config 
            services.AddMixAuthorize<MixCmsAccountContext>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMixCors();
            app.UseMixApps(Assembly.GetExecutingAssembly(), Configuration, env.ContentRootPath, env.IsDevelopment());
        }
    }
}
