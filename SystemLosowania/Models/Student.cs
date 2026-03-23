namespace SystemLosowania.Models;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsPresent { get; set; }
    public int TimesAnswered { get; set; }
    public int LuckyNumber { get; set; }

    public Student()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        IsPresent = true;
        TimesAnswered = 0;
        LuckyNumber = 0;
    }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}
