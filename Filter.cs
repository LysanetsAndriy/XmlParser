using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlParser
{
    public class Filter
    {
        public Filter() 
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

        public bool UseFilterToWork(Work work)
        {
            bool is_Author = work.Author?.ToLower().Contains(this.Author.ToLower()) ?? false;
            bool is_WorkName = work.WorkName?.ToLower().Contains(this.WorkName.ToLower()) ?? false;
            bool is_Faculty = work.Faculty?.ToLower().Contains(this.Faculty.ToLower()) ?? false;
            bool is_Department = work.Department?.ToLower().Contains(this.Department.ToLower()) ?? false;
            bool is_WorkType = work.WorkType?.ToLower().Contains(this.WorkType.ToLower()) ?? false;
            bool is_Year = work.Year?.Contains(this.Year) ?? false;

            return is_Author && is_WorkName && is_Faculty && is_Department && is_WorkType && is_Year;
        }
    }
}
