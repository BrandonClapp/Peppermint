using System;

namespace Peppermint.Core.Data
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
