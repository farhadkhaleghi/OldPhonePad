using ConsoleManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace ConsoleManager
    {
        public abstract class ConsoleManagerBase : IConsoleManager
        {
            public abstract void Clear();
            public abstract ConsoleKeyInfo ReadKey();
            public abstract string ReadLine();
            public abstract void Write(string value);
            public abstract void WriteLine(string value);
        }
    }
