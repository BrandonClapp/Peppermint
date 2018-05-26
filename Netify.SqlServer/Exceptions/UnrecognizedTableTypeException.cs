using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.SqlServer.Exceptions
{
    class UnrecognizedTableTypeException : Exception
    {
        public UnrecognizedTableTypeException() : base()
        {
        }

        public UnrecognizedTableTypeException(string message) : base(message)
        {
        }
    }
}
