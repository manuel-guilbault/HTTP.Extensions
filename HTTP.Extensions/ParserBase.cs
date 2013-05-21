using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions
{
    public class ParserBase
    {
        private string value;
        private int position;

        protected void Initialize(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            this.value = value;
            this.position = 0;
        }

        protected bool IsAtEnd()
        {
            return position >= value.Length;
        }

        protected string Peek(int length)
        {
            return value.Substring(position, Math.Min(value.Length - position, length));
        }

        protected bool Peek(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return Peek(value.Length) == value;
        }

        protected string Read(int length)
        {
            var result = Peek(length);
            position += length;
            return result;
        }

        protected void Read(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            if (Read(value.Length) != value) throw new ParserException();
        }

        protected string ReadUntil(string value)
        {
            int end = position;
            while (!IsAtEnd() && this.value.Substring(end, Math.Min(this.value.Length - end, value.Length)) != value) ++end;

            var result = Peek(end - position);
            position = end;
            return result;
        }

        protected void SkipWhiteSpaces()
        {
            while (!IsAtEnd() && value[position] == ' ') position++;
        }
    }
}
