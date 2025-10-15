using Serilog;
using System.Reflection;

namespace EFSample.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information($"App info: {Assembly.GetExecutingAssembly().FullName}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
