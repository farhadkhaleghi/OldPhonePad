using Ninject;

using ProgramManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OldPhonePad
{
    class Program
    {


        private static IProgramManager m_ProgramManager = null;

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            m_ProgramManager = kernel.Get<IProgramManager>();
            m_ProgramManager.Run();

        }
    }
}
