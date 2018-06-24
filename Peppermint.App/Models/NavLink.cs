using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.Models
{
    public class NavLink
    {
        public string Label { get; set; }
        public string Location { get; set; }

        public IEnumerable<NavLink> SubItems { get; set; }
    }
}
