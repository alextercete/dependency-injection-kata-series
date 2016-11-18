using System;
using Autofac;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    internal class ProgramModule : Module
    {
        private readonly string _pattern;
        private readonly bool _useColors;

        public ProgramModule(bool useColors, string pattern)
        {
            _useColors = useColors;
            _pattern = pattern;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetCharacterWriter(_useColors));
            builder.RegisterType<PatternWriter>();
            builder.RegisterInstance(GetSquarePainter(_pattern));
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