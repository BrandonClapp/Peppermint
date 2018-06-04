using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    public class DataLocation : Attribute
    {
        private readonly string _location;

        public DataLocation(string location)
        {
            _location = location;
        }

        public string GetLocation()
        {
            return _location;
        }
    }
}
