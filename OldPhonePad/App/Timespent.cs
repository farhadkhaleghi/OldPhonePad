using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad
{
    public class Timespent
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
}
