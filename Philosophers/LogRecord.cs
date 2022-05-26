using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosophers
{
    public class LogRecord
    {
        public int Attempts { get; set; } = 0;
        public int Eatings { get; set; } = 0;
        public int EatTime { get; set; } = 0;
        public int ThinkTime { get; set; } = 0;
    }
}
