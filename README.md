<h1 dir="auto">Old phone keypad simulator</h1>
<div style="width:50%;float:left;">
<p  style="width:50%;float:left;">
This project is a simulation of the old phone keypad, which is implemented with the C# programming language. In the main classes and methods, tried to use basic commands to make it easy to convert other programming languages.
  
</p>
  </div>
<div style="width:50%;float:left;">
<img src="https://user-images.githubusercontent.com/28364694/220209037-89d83ed4-7411-4c26-a27e-0ffb87631241.png" />
  </div>
<h2> Description</h2>
<p>
  The key function of old phones keypad was like this Each button has a number to identify it and pressing a button 
multiple times will
cycle through the letters on it allowing 
each button to represent more than one letter.
</p>
<p>
  For example, pressing 2 once will return ‘A’ but pressing twice 
in succession will return
‘B’.
And have to pause for a second in order to type two characters from the same 
button
after each other: “222 2 22” -> “CAB”. 
  </p>
  <p>Alson"#" for send and "*" to remove the last character have been used. 
  <h1>Dependencies</h1>
 <p> <a href="https://www.nuget.org/packages/Ninject" rel="nofollow"><code>Ninject</code></a></p>
<p><a href="https://www.nuget.org/packages/xunit"  rel="nofollow"><code>Xunit</code></a></p>
<h1>
  Code clarification
  </h1>
  <h2>Time spent class</h2>
  <pre><code>    public class Timespent
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
</code></pre>
<p>
  This class created for calculate time gap. It just include start attribute
which initialize to current date time by constructor function.
ReSet() method will set start to current time at required to reset and
getTimeSpent() will
return time spent.
  </p>
<h2>
  keyDecoder class</h2>
   <pre><code>public class keyDecoder
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
</code></pre>
<p>keyDecoder class responsible to encode digit keys to alphabet.
With in constructor method give digit key convert to integer for using as index also two special keys space and * converted as separated because this keys not returns digit char.</p><p>
AddCount() method used to calculate count of same keys in sequence for example for “9999” entry _keyCodewill set to 9 and count will be 3(count calculated from 0).</p><p>DecodeKey() method will return corresponded char based on _keyCode and calculated count of replica.
Keypad Array defined to holds corresponding chars to each digit key like keypad[9][3] will return ‘z’. </p><p>And also I have used divide remaining of to keep count in range if count of each replica was more than existing char count in sequence. 
  For example if user enter “22222#” in one 
second without gap the count will be 1 and returned char will be ‘b’.</p>
<h2>keypadEntry() Function</h2>
  <pre><code>
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
        </code></pre>
        <p>This function used for make space between entered keys sequence after one second.This function will read keys until user enter the ‘#’ . The First
if(string.IsNullOrEmpty(res))doing check if entered key is first one then will reset timer because timer was started
before first key. Second if will check time spend if was more than 1 second will add space and reset timer. Also checking (Keychar != '#‘ ) will prevent to add space before ‘#’ char.</p>
<h2>OldPhonePad() Function</h2>
  <pre><code>
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
        </code></pre>
        <p>
        In this function I defined list of keyEncoderclass then inside the loop create instance of
class and adding to list when current key is not equal to previous key or that is first key
otherwise will call AddCount() method of last inserted instance to list then we will enable to counting replica of each digit key.</p><p>Also when inputted char was ‘*’ I just remove last
inserted instance to list then last char before ‘*’ will remove as well.
In another for() loop, I invoke all list
instances EncodeKey() method and hold
encoded keys in result as sequence then result will include decoded message.
</p>
<h1>Test</h1>
 <pre><code>
 public class UnitTest1
    {
        private ConsoleManagerStub _cmSt = null;
        private ProgramManagerTest _pmSt = null;
        private readonly ProgramManager.IProgramManager _pm;
        [Fact]
        public void OldPhonePadAndKeyDecoderClassTest()
        {
            _cmSt = new ConsoleManagerStub();
            _pmSt = new ProgramManagerTest(_cmSt);
  
            var testInputList = new List<string>
            {
                "4433555 555666#",
                "8 88777444666*664#",
                "222 2 22#",
                "444 777 666 6607777 6663338#"
            };
            var expectedOutput = new List<string>
            {
                "hello",
                "turing",
                "cab",
                "iron soft"
            };
            
            for(int i=0;i< testInputList.Count;i++ )
            {
                Assert.Equal(
                     _pmSt.OldPhonePad(testInputList[i]), expectedOutput[i] );
            }
        }
    }
</code></pre>
