namespace Mix.Account
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return MixCmsHelper.CreateHostBuilder<Startup>(args);
        }
    }
}
