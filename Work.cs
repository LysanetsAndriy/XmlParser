using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlParser
{
    public struct Work
    {
        public Work()
        {
            Author = string.Empty;
            WorkName = string.Empty;
            Faculty = string.Empty;
            Department = string.Empty;
            WorkType = string.Empty;
            Year = string.Empty;
        }
        public string Author { get; set; }
        public string WorkName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string WorkType { get; set; }
        public string Year { get; set; }

    }
}
