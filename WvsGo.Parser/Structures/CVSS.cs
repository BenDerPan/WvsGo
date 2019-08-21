using System;
using System.Collections.Generic;
using System.Text;

namespace WvsGo.Parser.Structures
{
    public class CVSS
    {
        public string Descriptor { get; set; }
        public string Score { get; set; }
        public string AV { get; set; }
        public string AVdesc { get; set; }
        public string AC { get; set; }
        public string ACdesc { get; set; }
        public string Au { get; set; }
        public string Audesc { get; set; }
        public string C { get; set; }
        public string Cdesc { get; set; }
        public string I { get; set; }
        public string Idesc { get; set; }
        public string A { get; set; }
        public string Adesc { get; set; }
        public string E { get; set; }
        public string Edesc { get; set; }
        public string RL { get; set; }
        public string RLdesc { get; set; }
        public string RC { get; set; }
        public string RCdesc { get; set; }
    }
}
