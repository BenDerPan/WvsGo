using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WvsGo.Parser.Structures
{
    public class ReportItem
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("color")]
        public string Color { get; set; }

        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string Details { get; set; }
        public string Affects { get; set; }
        public string Parameter { get; set; }
        public string AOP_SourceFile { get; set; }
        public string AOP_SourceLine { get; set; }
        public string AOP_Additional { get; set; }
        public string IsFalsePositive { get; set; }
        public string Severity { get; set; }
        public string Type { get; set; }
        public string Impact { get; set; }
        public string Description { get; set; }
        public string DetailedInformation { get; set; }
        public string Recommendation { get; set; }
        public TechnicalDetail TechnicalDetails { get; set; }

        public List<CVE> CVEList { get; set; }
        public CVSS CVSS { get; set; }
        public CVSS3 CVSS3 { get; set; }
        public List<Reference> References { get; set; }
        public ReportItem()
        {
            TechnicalDetails = new TechnicalDetail();
            CVEList = new List<CVE>();
            CVSS = new CVSS();
            CVSS3 = new CVSS3();
            References = new List<Reference>();
        }
    }
}
