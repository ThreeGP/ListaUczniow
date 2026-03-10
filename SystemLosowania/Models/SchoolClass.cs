using System.Collections.Generic;

namespace SystemLosowania.Models
{
    public class SchoolClass
    {
        public string ClassName { get; set; }
        public List<Student> Students { get; set; }

        public SchoolClass()
        {
            // pusty konstruktor
            ClassName = "";
            Students = new List<Student>();
        }

        public SchoolClass(string className)
        {
            // konstruktor z nazwa klasy
            ClassName = className;
            Students = new List<Student>();
        }
    }
}
