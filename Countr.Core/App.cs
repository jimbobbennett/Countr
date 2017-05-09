using MvvmCross.Platform.IoC;

namespace Countr.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
			    .EndingWith("Repository")
			    .AsInterfaces()
			    .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.CountersViewModel>();
        }
    }
}
