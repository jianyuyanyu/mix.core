using Mix.Lib.Helpers;

namespace Mixcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return MixCmsHelper.CreateHostBuilder<Startup>(args);
        }
    }
}
