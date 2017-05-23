using Autofac;
using System.Configuration;
using Tamagochi.Core;
using Tamagochi.Core.Factories;
using Tamagochi.Core.GameTimer;
using Tamagochi.Core.Models;
using Tamagochi.Core.Serializer;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.DI
{
    public class AutofacInitializer
    {
        public static void Initialize(ContainerBuilder builder, ApplicationSettingsBase appSettings)
        {
            builder.RegisterType<TimerProvider>().As<ITimerProvider>().SingleInstance();
            builder.Register(c => c.Resolve<ITimerProvider>().GetTimer());
            builder.RegisterType<TamagochiSerializer>().As<ISerializer>();
            builder.RegisterType<GameContextProvider>().As<IGameContextProvider>().SingleInstance();
            builder.Register(c =>
            {
                var timer = c.Resolve<AbstractTamagochiTimer>();
                var petFactory = c.Resolve<AbstractPetFactory>();
                return new GameFactory(petFactory, timer);
            }).As<AbstractGameFactory>().SingleInstance();

            builder.Register(c =>
            {
                var provider = c.Resolve<IGameContextProvider>();
                var serializer = c.Resolve<ISerializer>();
                var settings = c.Resolve<ISettings>();
                return provider.GetGameContext(serializer, settings);
            }).SingleInstance();

            builder.RegisterType<PetFactory>().As<AbstractPetFactory>().SingleInstance();
            builder.RegisterType<TamagochiSettingsProvider>().As<ISettingsProvider>().SingleInstance();
            builder.Register(c => c.Resolve<ISettingsProvider>().GetSettings(appSettings)).SingleInstance();
        }
    }
}