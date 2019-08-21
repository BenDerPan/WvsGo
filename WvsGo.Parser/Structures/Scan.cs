using System;
using System.Collections.Generic;
using System.Text;

namespace WvsGo.Parser.Structures
{
    public class Scan
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string StartURL { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string ScanTime { get; set; }
        public string Aborted { get; set; }
        public string Responsive { get; set; }
        public string Banner { get; set; }
        public string Os { get; set; }
        public string WebServer { get; set; }
        public string Technologies { get; set; }
        public List<KBItem> KBase { get; set; }
        public Crawler Crawler { get; set; }
        public List<ReportItem> ReportItems { get; set; }
        public Scan()
        {
            KBase = new List<KBItem>();
            Crawler = new Crawler();
            ReportItems = new List<ReportItem>();
        }
    }
}
