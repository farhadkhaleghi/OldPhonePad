using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad
{
   public class keyDecoder
    {
        private int _count { get; set; } = 0;
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
            _count++;
        }
        public char DecodeKey()
        {
            String[] keypad = { " ", "&'(", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz", "*" };
            if (_keyCode == 7 || _keyCode == 9)
            {
                _count = _count % 4;
            }
            else
            {
                _count = _count % 3;
            }
            return keypad[_keyCode][_count];
        }
       

    }
}
