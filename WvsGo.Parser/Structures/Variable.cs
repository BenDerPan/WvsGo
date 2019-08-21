using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class Variable
    {
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public string Type { get; set; }
    }
}
