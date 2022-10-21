using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldkeyPad
{
    class Program
    {
        class Timespent
        {
            private DateTime start { get; set; }
            public Timespent()
            {
                start = DateTime.Now;
            }

            public void ReSet()
            {
                start = DateTime.Now;
            }

            public int getTimeSpent()
            {
                var seconds = (DateTime.Now - start).TotalMilliseconds;
                return (int)seconds;
            }
        }
        class keyDecoder
        {
            private int count { get; set; } = 0;
            private int _keyCode { get; set; }
            public keyDecoder()
            { }
            public keyDecoder(char keyCode)
            {
                if (keyCode == ' ')
                    _keyCode = 0;
                else if (keyCode == '*')
                    _keyCode = 10;
                else
                    _keyCode = (int)Char.GetNumericValue(keyCode);
            }
            public void AddCount()
            {
                count++;
            }
            public char DecodeKey()
            {
                String[] keypad = { " ", "&'(", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz", "*" };
                if (_keyCode == 7 || _keyCode == 9)
                {
                    count = count % 4;
                }
                else
                {
                    count = count % 3;
                }
                return keypad[_keyCode][count];
            }
        }
        static string keypadEntry()
        {
            char Keychar = '0';
            string res = string.Empty;
            Timespent ts = new Timespent();
            while (Keychar != '#')
            {
                Keychar = Console.ReadKey().KeyChar;
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
        static void Main(string[] args)
        {
            string input = keypadEntry();
            Console.Clear();
            Console.WriteLine(input);
            Console.WriteLine(OldPhonePad(input));
            Console.ReadKey();
        }
    }
}
