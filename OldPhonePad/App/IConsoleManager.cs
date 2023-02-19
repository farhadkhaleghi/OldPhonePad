using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace ConsoleManager
    {
        public interface IConsoleManager
        {
            void Write(string value);
            void WriteLine(string value);
            ConsoleKeyInfo ReadKey();
            string ReadLine();
            void Clear();
        }
    }
