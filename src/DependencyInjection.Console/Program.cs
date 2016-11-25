using Autofac;

namespace DependencyInjection.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = ProgramOptions.Parse(args);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ProgramModule(options));
            var container = builder.Build();

            var app = container.Resolve<PatternApp>();
            app.Run(options.Width, options.Height);
        }
    }
}
