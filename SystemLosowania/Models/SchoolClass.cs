namespace SystemLosowania.Models;

public class SchoolClass
{
    public string ClassName { get; set; }
    public List<Student> Students { get; set; }

    public SchoolClass()
    {
        ClassName = string.Empty;
        Students = new List<Student>();
    }

    public SchoolClass(string className)
    {
        ClassName = className;
        Students = new List<Student>();
    }
}
