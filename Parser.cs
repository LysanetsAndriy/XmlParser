using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlParser
{
    public abstract class Parser
    {
        protected IList<Work> Works;
        public abstract bool Load(Stream input, XmlReaderSettings settings);
        public abstract IList<Work> Find(Filter filter);
        public IList<Work> GetAllWorks() 
        {
            return Works.ToList();
        }
    }
}
