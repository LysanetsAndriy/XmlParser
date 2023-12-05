using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Linq;

namespace XmlParser
{
    class DOM : Parser
    {
        public DOM() 
        {
            Works = new List<Work>();
        }
        public override bool Load(Stream input, XmlReaderSettings settings)
        {
            Works.Clear();
            var document = new XmlDocument();
            using var reader = XmlReader.Create(input, settings);
            try
            {
                document.Load(reader);
                if (document == null || document.DocumentElement == null)
                {
                    return true;
                }
                var serializer = new XmlSerializer(typeof(Work));
                foreach (XmlNode child in document.DocumentElement.ChildNodes)
                {
                    if (serializer.Deserialize(new StringReader(child.OuterXml)) is Work work)
                    {
                        Works.Add(work);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        public override IList<Work> Find(Filter filter)
        {
            return Works.Where(filter.UseFilterToWork).ToList();
        }
    }
}
