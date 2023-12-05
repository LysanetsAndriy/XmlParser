using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace XmlParser
{
    class LINQ : Parser
    {
        public LINQ() 
        {
            Works = new List<Work>();
        }
        public override bool Load(Stream input, XmlReaderSettings settings)
        {
            try
            {
                using (StreamReader reader = new StreamReader(input))
                {
                    XDocument xmlDocument = XDocument.Load(reader);

                    ExtractWorks(xmlDocument);

                    return true; // Loading successful
                }
            }
            catch (Exception ex)
            {
                return false; // Loading failed
            }
        }

        private void ExtractWorks(XDocument xmlDocument)
        {
            var works = from workElement in xmlDocument.Descendants("Work")
                        select new Work
                        {
                            Author = workElement.Element("Author")?.Value ?? string.Empty,
                            WorkName = workElement.Element("WorkName")?.Value ?? string.Empty,
                            Faculty = workElement.Element("Faculty")?.Value ?? string.Empty,
                            Department = workElement.Element("Department")?.Value ?? string.Empty,
                            WorkType = workElement.Element("WorkType")?.Value ?? string.Empty,
                            Year = workElement.Element("Year")?.Value ?? string.Empty
                        };

            Works = works.ToList();
        }
        public override IList<Work> Find(Filter filter)
        {
            return Works.Where(filter.UseFilterToWork).ToList();
        }
    }
}
