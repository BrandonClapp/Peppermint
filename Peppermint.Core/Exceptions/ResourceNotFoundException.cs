using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Peppermint.Core.Exceptions
{
    public class ResourceNotFoundException : FileNotFoundException
    {
        public ResourceNotFoundException() : base()
        {
        }
    }
}
