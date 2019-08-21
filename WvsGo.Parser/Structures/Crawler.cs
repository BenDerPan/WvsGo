using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class Crawler
    {
        [XmlAttribute()]
        public string StartUrl { get; set; }
        public List<Variable> Cookies { get; set; }
        public List<SiteFile> SiteFiles { get; set; }
        public Crawler()
        {
            SiteFiles = new List<SiteFile>();
            Cookies = new List<Variable>();
        }
    }
}
