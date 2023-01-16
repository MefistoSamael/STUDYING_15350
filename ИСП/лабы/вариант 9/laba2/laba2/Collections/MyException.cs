using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2.Collections
{
    public class MyException : ArgumentException
    {
        public object Value { get; set; }
        public MyException(string message, object Value) : base(message)
        {
            this.Value = Value;
        }
    }
}
