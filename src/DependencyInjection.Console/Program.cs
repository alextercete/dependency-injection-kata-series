using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var writer = new AsciiWriter();
            var characterWriter = useColors ? (ICharacterWriter)new ColorWriter(writer) : writer;

            var patternWriter = new PatternWriter(characterWriter);

            var squarePainter = GetSquarePainter(pattern);

            var patternGenerator = new PatternGenerator(squarePainter);

            var app = new PatternApp(patternWriter, patternGenerator);

            app.Run(width, height);
        }

        private static ISquarePainter GetSquarePainter(string pattern)
        {
            var type = GetDerivedTypesOf<ISquarePainter>().FirstOrDefault(
                t => t.Name.Equals($"{pattern}SquarePainter", StringComparison.InvariantCultureIgnoreCase));

            if (type == null)
            {
                throw new Exception($"Pattern {pattern} unknown");
            }
            return (ISquarePainter) Activator.CreateInstance(type);
        }

        private static IEnumerable<Type> GetDerivedTypesOf<T>()
        {
            var superType = typeof(T);
            var assembly = Assembly.GetAssembly(superType);
            return assembly.GetTypes().Where(type => type != superType && superType.IsAssignableFrom(type));
        }
    }
}
