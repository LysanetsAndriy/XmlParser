using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace XmlParser
{
    public partial class MainPage : ContentPage 
    {
        private async void ExitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }
        private async void OpenButton_Clicked(object sender, EventArgs e)
        {
            string projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            projectFolderPath = Path.GetFullPath(Path.Combine(projectFolderPath, "..", "..", "..", "..", ".."));
            string fullPath = Path.Combine(projectFolderPath, "works.xsd");

            if (settings == null)
            {
                string xsdFilePath = fullPath;
                ImportValidationSchema(xsdFilePath);
            }
            var loadPicker = new PickOptions()
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".xml" } },
                }),
            };

            File = await FilePicker.PickAsync(loadPicker);
            if (File == null)
            {
                await DisplayAlert("Помилка", "Помилка під час відкриття файлу!", "Ок");
                return;
            }
            if (parser == null)
            {
                await DisplayAlert("Помилка", "Тип парсера не вибраний!", "Ок");
                return;
            }

            ClearResults();
            await ValidateFile();
            AllWorksList = parser.GetAllWorks();
            FilteredWorksList = AllWorksList;
            if (AllWorksList.Count > 0)
            {
                Table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                for (int i = 1; i <= AllWorksList.Count; ++i)
                {

                    DisplayResult(AllWorksList[i - 1], i);
                }
            }
            else
            {
                await DisplayAlert("Увага", "Цей файл порожній!", "Ок");
            }
            await DisplayAlert("Відкрито", "Файл " + File.FileName + " був завантажений успішно!", "Ок");
        }
        private void DisplayResult(Work result, int rowIndex)
        {
            Table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            AddLabelToGrid(result.Author, rowIndex, 0);
            AddLabelToGrid(result.WorkName, rowIndex, 1);
            AddLabelToGrid(result.Faculty, rowIndex, 2);
            AddLabelToGrid(result.Department, rowIndex, 3);
            AddLabelToGrid(result.WorkType, rowIndex, 4);
            AddLabelToGrid(result.Year, rowIndex, 5);
        }
        private void AddLabelToGrid(string value, int row, int column)
        {
            var label = new Label
            {
                Text = value,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10
            };
            
            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            Table.Children.Add(label);
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (File == null)
            {
                await DisplayAlert("Помилка", "Помилка під час відкриття файлу!", "Ок");
                return;
            }

            if (exporter == null)
            {
                exporter = new();

                string projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                projectFolderPath = Path.GetFullPath(Path.Combine(projectFolderPath, "..", "..", "..", "..", ".."));
                string fullPath = Path.Combine(projectFolderPath, "works.xsl");
                exporter.Load(fullPath);
            }

            if (FilteredWorksList != null && FilteredWorksList.Count > 0)
            {
                // Create a new XDocument to store the filtered works
                XDocument filteredXml = new XDocument(new XElement("Works"));

                foreach (var work in FilteredWorksList)
                {
                    var workElement = new XElement("Work",
                        new XElement("Author", work.Author),
                        new XElement("WorkName", work.WorkName),
                        new XElement("Faculty", work.Faculty),
                        new XElement("Department", work.Department),
                        new XElement("WorkType", work.WorkType),
                        new XElement("Year", work.Year)
                    );

                    filteredXml.Root.Add(workElement);
                }

                // Save the filtered works to a new XML file
                string filteredFilePath = Path.Combine(Path.GetDirectoryName(File.FullPath), "filteredWorks.xml");
                filteredXml.Save(filteredFilePath);

                // Transform the XML file to HTML using XSLT
                using var stream = new MemoryStream(Encoding.Default.GetBytes(""));
                var result = await fileSaver.SaveAsync(File.FileName.Split(".")[0] + "_filtered.html", stream, new CancellationTokenSource().Token);
                exporter.Transform(filteredFilePath, result.FilePath);

                await DisplayAlert("Збережено", "Файл був збережений успішно!", "Ок");
            }
            else
            {
                await DisplayAlert("Увага", "Немає відфільтрованих робіт для збереження!", "Ок");
            }
        }
        private async void FindButton_Clicked(object sender, EventArgs e)
        {
            if (File == null)
            {
                await DisplayAlert("Помилка", "Вхідний файл не вибраний!", "Ок");
                return;
            }

            if (parser == null)
            {
                await DisplayAlert("Помилка", "Парсер не вибраний!", "Ок");
                return;
            }

            var filterOptions = GetFilters();
            ClearResults();

            FilteredWorksList = parser.Find(filterOptions);
            Table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (int i = 1; i <= FilteredWorksList.Count; ++i)
            {
                DisplayResult(FilteredWorksList[i - 1], i);
            }
        }
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            ClearFilters();
        }
        private async void Parser_Selected(object sender, EventArgs e)
        {
            if (ParserPicker.SelectedIndex == 0)
            {
                parser = new DOM();
            }
            if (ParserPicker.SelectedIndex == 1)
            {
                parser = new SAX();
            }
            if (ParserPicker.SelectedIndex == 2)
            {
                parser = new LINQ();
            }
            await ValidateFile();
        }
    }
}
