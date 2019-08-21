using System;
using System.Collections.Generic;
using System.Text;

namespace WvsGo.Parser.Structures
{
    public class KBItem
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Reference> References { get; set; }
    }
}
