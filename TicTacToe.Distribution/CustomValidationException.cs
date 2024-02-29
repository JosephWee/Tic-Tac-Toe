using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException()
        {
        }

        public CustomValidationException(string message)
            : base(message)
        {
        }

        public CustomValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public CustomValidationException(Exception inner)
            : base(inner.Message, inner)
        {
        }
    }
}
