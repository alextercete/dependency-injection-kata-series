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
            builder.RegisterType<CircleSquarePainter>().Named<ISquarePainter>("circle");
            builder.RegisterType<OddEvenSquarePainter>().Named<ISquarePainter>("oddeven");
            builder.RegisterType<WhiteSquarePainter>().Named<ISquarePainter>("white");
            builder.Register(c => c.ResolveNamed<ISquarePainter>(_options.Pattern));
            builder.RegisterType<PatternGenerator>();
            builder.RegisterType<PatternApp>();
        }

        private static ICharacterWriter GetCharacterWriter(bool useColors)
        {
            var writer = new AsciiWriter();
            return useColors ? (ICharacterWriter)new ColorWriter(writer) : writer;
        }
    }
}