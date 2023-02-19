using ConsoleManager;
using OldPhonePad;
using System;
using System.Collections.Generic;


namespace ProgramManager
{
    public class ProgramManager : ProgramManagerBase
    {
        private readonly IConsoleManager _cm;

        public ProgramManager(IConsoleManager consoleManager)
        {
            _cm = consoleManager;
        }
        public static String OldPhonePad(string input)
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

       
        static string keypadEntry(IConsoleManager _cm)
        {
            char Keychar = '0';
            string res = string.Empty;
            Timespent ts = new Timespent();
            while (Keychar != '#')
            {
                Keychar = _cm.ReadKey().KeyChar;
                if (string.IsNullOrEmpty(res))
                    ts.ReSet();
                if ((ts.getTimeSpent() > 999) && (Keychar != '#'))
                {
                    res += ' ';
                    ts.ReSet();
                }
                res += Keychar;
            }
            return res;
        }
        public override void Run()
        {
            string input = keypadEntry(_cm);
            _cm.Clear();
            _cm.WriteLine(input);
            _cm.WriteLine(OldPhonePad(input));
            _cm.ReadKey();
        }
    }
}    
   
    
