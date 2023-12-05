using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml;

namespace XmlParser
{
    public partial class MainPage : ContentPage
    {
        private void ClearResults()
        {
            Table.Children.Clear();
            Table.RowDefinitions.Clear();

            Table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            AddLabelToGrid("Автор", 0, 0);
            AddLabelToGrid("Назва матеріалу", 0, 1);
            AddLabelToGrid("Факультет", 0, 2);
            AddLabelToGrid("Кафедра", 0, 3);
            AddLabelToGrid("Вид матеріалу", 0, 4);
            AddLabelToGrid("Рік створення", 0, 5);
        }
        private void ClearFilters()
        {
            AuthorCheckbox.IsChecked = false;
            WorkNameCheckbox.IsChecked = false;
            FacultyCheckbox.IsChecked = false;
            DepartmentCheckbox.IsChecked = false;
            WorkTypeCheckbox.IsChecked = false;
            YearCheckbox.IsChecked = false;

            AuthorEntry.Text = "";
            WorkNameEntry.Text = "";
            FacultyEntry.Text = "";
            DepartmentEntry.Text = "";
            WorkTypeEntry.Text = "";
            YearEntry.Text = "";
        }
        private Filter GetFilters()
        {
            Filter filter = new Filter();

            if (AuthorCheckbox.IsChecked)
            {
                filter.Author = AuthorEntry.Text?.Trim() ?? "";
            }

            if (WorkNameCheckbox.IsChecked)
            {
                filter.WorkName = WorkNameEntry.Text?.Trim() ?? "";
            }

            if (FacultyCheckbox.IsChecked)
            {
                filter.Faculty = FacultyEntry.Text?.Trim() ?? "";
            }

            if (DepartmentCheckbox.IsChecked)
            {
                filter.Department = DepartmentEntry.Text?.Trim() ?? "";
            }

            if (WorkTypeCheckbox.IsChecked)
            {
                filter.WorkType = WorkTypeEntry.Text?.Trim() ?? "";
            }

            if (YearCheckbox.IsChecked)
            {
                filter.Year = YearEntry.Text?.Trim() ?? "";
            }

            return filter;
        }
        private void ImportValidationSchema(string filepath)
        {
            var schema = new XmlSchemaSet();
            schema.Add("", filepath);

            settings = new XmlReaderSettings
            {
                Schemas = schema
            };
            settings.ValidationEventHandler += (object? sender, ValidationEventArgs e) =>
            {
                if (e.Severity == XmlSeverityType.Error) throw new Exception();
            };
            settings.ValidationType = ValidationType.Schema;
        }
        private async Task ValidateFile()
        {
            if (parser == null || File == null)
            {
                return;
            }
            if (parser.Load(await File.OpenReadAsync(), settings))
            {
                return;
            }

            File = null;
            await DisplayAlert("Помилка", "Файл не відповідяє XSD схемі", "Ок");
        }
    }
}
