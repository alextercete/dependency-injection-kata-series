using NDesk.Options;

namespace DependencyInjection.Console
{
    internal class ProgramOptions
    {
        public int Width { get; private set; } = 25;

        public int Height { get; private set; } = 15;

        public bool UseColors { get; private set; } = false;

        public string Pattern { get; private set; } = "circle";

        private ProgramOptions()
        {
        }

        public static ProgramOptions Parse(string[] args)
        {
            var options = new ProgramOptions();

            var optionSet = new OptionSet
            {
                {"c|colors", value => options.UseColors = value != null},
                {"w|width=", value => options.Width = int.Parse(value)},
                {"h|height=", value => options.Height = int.Parse(value)},
                {"p|pattern=", value => options.Pattern = value}
            };
            optionSet.Parse(args);

            return options;
        }
    }
}