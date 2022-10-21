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
        class keyEncoder
        {
            private int count { get; set; } = 0;
            private int _keyCode { get; set; }
            public keyEncoder()
            { }
            public keyEncoder(char keyCode)
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
            public char EncodeKey()
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
            List<keyEncoder> keyEncoderList = new List<keyEncoder>();
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '#') break;
                if (input[index] == '*' && keyEncoderList.Count > 0)
                {
                    keyEncoderList.RemoveAt(keyEncoderList.Count - 1);
                }
                if (input[index] != ' ' && input[index] != '*')
                {
                    if (index == 0 || input[index] != input[index - 1])
                    {
                        keyEncoderList.Add(new keyEncoder(input[index]));
                    }
                    else
                    {
                        keyEncoderList[keyEncoderList.Count - 1].AddCount();
                    }
                }
            }
            string result = string.Empty;
            for (int index = 0; index < keyEncoderList.Count; index++)
            {
                result += keyEncoderList[index].EncodeKey();
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
