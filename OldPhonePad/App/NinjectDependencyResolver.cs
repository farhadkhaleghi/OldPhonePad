using Ninject.Modules;
using ConsoleManager;
using ProgramManager;

namespace MyConsoleApp
{
    public class NinjectDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IConsoleManager>().To<ConsoleManager.ConsoleManager>();
            Bind<IProgramManager>().To<ProgramManager.ProgramManager>();
        }
    }
}