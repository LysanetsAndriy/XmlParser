using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlParser
{
    class SAX : Parser
    {
        public SAX() 
        {
            Works = new List<Work>();
        }
        public override bool Load(Stream inputStream, XmlReaderSettings settings)
        {
            Works.Clear();
            try
            {
                var reader = XmlReader.Create(inputStream, settings);
                while (reader.Read())
                {
                    if (!(reader.NodeType == XmlNodeType.Element && reader.Name == "Work"))
                    {
                        continue;
                    }

                    var work = new Work();
                    SkipToText(reader);
                    work.Author = reader.Value;
                    SkipToText(reader);
                    work.WorkName = reader.Value;
                    SkipToText(reader);
                    work.Faculty = reader.Value;
                    SkipToText(reader);
                    work.Department = reader.Value;
                    SkipToText(reader);
                    work.WorkType = reader.Value;
                    SkipToText(reader);
                    work.Year = reader.Value;

                    Works.Add(work);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SkipToText(XmlReader reader)
        {
            do
            {
                if (!reader.Read())
                {
                    throw new Exception();
                }
            } while (reader.NodeType != XmlNodeType.Text);
        }

        public override IList<Work> Find(Filter filter)
        {
            return Works.Where(filter.UseFilterToWork).ToList();
        }
    }
}
