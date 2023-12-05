using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui;
using System.Xml;
using System.Xml.Xsl;

namespace XmlParser
{
    public partial class MainPage : ContentPage
    {
        private FileResult File;
        private Parser parser;
        private XmlReaderSettings settings;
        private XslCompiledTransform exporter;
        private IFileSaver fileSaver;
        private IList<Work> AllWorksList;
        private IList<Work> FilteredWorksList;
        
        public MainPage()
        {
            InitializeComponent();
            ParserPicker.SelectedIndex = 0;
            fileSaver = FileSaver.Default;
        }

    }
}