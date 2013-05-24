using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Parsing
{
    public class Tokenizer
    {
        private string value;
        private int position;

        public Tokenizer(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            this.value = value;
            this.position = 0;
        }

        protected bool AreEqual(string value1, string value2, bool isCaseSensitive)
        {
            var comparison = isCaseSensitive
                ? StringComparison.InvariantCulture
                : StringComparison.InvariantCultureIgnoreCase;
            return string.Equals(value1, value2, comparison);
        }

        protected void Move(int offset = 1)
        {
            position += offset;
        }

        protected internal void Throw(string message, int offset = 0)
        {
            throw new ParsingException(message, value, position + offset);
        }

        public string StringValue
        {
            get { return value; }
        }

        public int Position
        {
            get { return position; }
        }

        public bool IsAtEnd(int offset = 0)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");

            return position + offset >= value.Length;
        }

        public char PeekChar(int offset = 0)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");
            if (IsAtEnd(offset)) Throw("End of string", offset);

            return value[position + offset];
        }

        public string Peek(int offset, int length)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");
            if (length <= 0) throw new ArgumentException("length must be greater than zero", "length");

            length = Math.Min(value.Length - (position + offset), length);
            return value.Substring(position + offset, length);
        }

        public string Peek(int length)
        {
            return Peek(0, length);
        }

        public bool IsNext(string value, bool isCaseSensitive = true)
        {
            if (value == null) throw new ArgumentNullException("value");

            return AreEqual(Peek(value.Length), value, isCaseSensitive);
        }

        public string Read(int length)
        {
            var result = Peek(length);
            Move(length);
            return result;
        }

        public void Read(string token, bool isCaseSensitive = true)
        {
            if (token == null) throw new ArgumentNullException("value");

            if (!AreEqual(Read(token.Length), token, isCaseSensitive))
            {
                Throw(string.Format("Not matching expected token ({0})", token));
            }
        }

        public string ReadUntil(string token, bool isCaseSensitive = true)
        {
            int offset = 0;
            while (!IsAtEnd(offset) && !AreEqual(Peek(offset, token.Length), token, isCaseSensitive))
            {
                ++offset;
            }

            if (IsAtEnd(offset))
            {
                return "";
            }

            return Read(offset);
        }

        public string ReadWhile(params char[] characters)
        {
            if (characters == null) throw new ArgumentNullException("characters");

            return ReadWhile(@char => characters.Contains(@char));
        }

        public string ReadWhile(Func<char, bool> predicate)
        {
            int offset = 0;
            while (!IsAtEnd(offset) && predicate(PeekChar(offset)))
            {
                ++offset;
            }
            return Read(offset);
        }

        public void SkipWhiteSpaces()
        {
            while (!IsAtEnd() && IsNext(" "))
            {
                Move();
            }
        }

        public override string ToString()
        {
            return Peek(int.MaxValue);
        }
    }
}
