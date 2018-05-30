using System;

namespace Peppermint.Core.Exceptions
{
    public class MissingExpectedAttributeException : Exception
    {
        public MissingExpectedAttributeException(string attributeName) 
            : base ($"Missing expected attribute: {attributeName}")
        {

        }
    }
}
