using System;
using Autofac;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;
using NDesk.Options;

namespace DependencyInjection.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var useColors = false;
            var width = 25;
            var height = 15;
            var pattern = "circle";

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(GetCharacterWriter(useColors));
            builder.RegisterType<PatternWriter>();
            builder.RegisterInstance(GetSquarePainter(pattern));
            builder.RegisterType<PatternGenerator>();
            builder.RegisterType<PatternApp>();
            var container = builder.Build();

            var app = container.Resolve<PatternApp>();
            app.Run(width, height);
        }

        private static ICharacterWriter GetCharacterWriter(bool useColors)
        {
            var writer = new AsciiWriter();
            return useColors ? (ICharacterWriter) new ColorWriter(writer) : writer;
        }

        private static ISquarePainter GetSquarePainter(string pattern)
        {
            switch (pattern)
            {
                case "circle":
                    return new CircleSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                case "white":
                    return new WhiteSquarePainter();
                default:
                    throw new ArgumentException($"Pattern '{pattern}' not found!");
            }
        }
    }
}
