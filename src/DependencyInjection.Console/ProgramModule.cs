using System;
using Autofac;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    internal class ProgramModule : Module
    {
        private readonly ProgramOptions _options;

        public ProgramModule(ProgramOptions options)
        {
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetCharacterWriter(_options.UseColors));
            builder.RegisterType<PatternWriter>();
            builder.RegisterInstance(GetSquarePainter(_options.Pattern));
            builder.RegisterType<PatternGenerator>();
            builder.RegisterType<PatternApp>();
        }

        private static ICharacterWriter GetCharacterWriter(bool useColors)
        {
            var writer = new AsciiWriter();
            return useColors ? (ICharacterWriter)new ColorWriter(writer) : writer;
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