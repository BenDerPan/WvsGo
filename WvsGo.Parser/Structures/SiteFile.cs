using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class SiteFile
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string FullURL { get; set; }
        public List<Variable> Inputs { get; set; }
        public List<Variation> Variations { get; set; }

        public SiteFile()
        {
            Inputs = new List<Variable>();
            Variations = new List<Variation>();
            
        }
    }
}
