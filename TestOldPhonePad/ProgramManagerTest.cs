using ConsoleManager;

using OldPhonePad;

using ProgramManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOldPhonePad
{
    internal class ProgramManagerTest 
    {
        private readonly IConsoleManager _cm;

        public ProgramManagerTest(IConsoleManager consoleManager)
        {
            _cm = consoleManager;
        }
        public  String OldPhonePad(string input)
        {
            List<keyDecoder> keyDecoderList = new List<keyDecoder>();
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '#') break;
                if (input[index] == '*' && keyDecoderList.Count > 0)
                {
                    keyDecoderList.RemoveAt(keyDecoderList.Count - 1);
                }
                if (input[index] != ' ' && input[index] != '*')
                {
                    if (index == 0 || input[index] != input[index - 1])
                    {
                        keyDecoderList.Add(new keyDecoder(input[index]));
                    }
                    else
                    {
                        keyDecoderList[keyDecoderList.Count - 1].AddCount();
                    }
                }
            }
            string result = string.Empty;
            for (int index = 0; index < keyDecoderList.Count; index++)
            {
                result += keyDecoderList[index].DecodeKey();
            }
            return result;
        }
  
    }
}
