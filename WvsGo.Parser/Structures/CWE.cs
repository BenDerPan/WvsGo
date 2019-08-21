using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class CWE
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlTextAttribute()]
        public string Text { get; set; }
    }
}
