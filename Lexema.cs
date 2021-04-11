using System;
using System.Collections.Generic;
using System.Text;

namespace _02_ObjectOrientedProgrammingExample
{
    abstract class Lexema
    {
        protected string _value;

        override public string ToString()
        {
            return _value;
        }
    }
}
