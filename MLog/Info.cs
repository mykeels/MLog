using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MLog
{
    public class Info
    {
        public long ID { get; set; }
        public long RelatedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string JsonData { get; set; }
        public XElement XmlData { get; set; }
        public string IpAddress { get; set; }
    }
}
