using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class ScanGroup
    {
        [XmlAttribute()]
        public string ExportedOn { get; set; }

        public Scan Scan { get; set; }

        public ScanGroup()
        {
            Scan = new Scan();
        }
    }
}
