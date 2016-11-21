using Autofac;
using Autofac.Core;
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
            builder.RegisterType<CircleSquarePainter>().Named<ISquarePainter>("circle");
            builder.RegisterType<OddEvenSquarePainter>().Named<ISquarePainter>("oddeven");
            builder.RegisterType<WhiteSquarePainter>().Named<ISquarePainter>("white");
            builder.Register(c => c.ResolveNamed<ISquarePainter>(_options.Pattern));

            builder.RegisterType<AsciiWriter>().Keyed<ICharacterWriter>(false);
            builder.RegisterType<ColorWriter>()
                .WithParameter(ResolvedParameter.ForKeyed<ICharacterWriter>(false))
                .Keyed<ICharacterWriter>(true);
            builder.Register(c => c.ResolveKeyed<ICharacterWriter>(_options.UseColors));

            builder.RegisterAssemblyTypes(ThisAssembly);
        }
    }
}