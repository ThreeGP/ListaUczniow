namespace SystemLosowania.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPresent { get; set; }
        public int TimesAnswered { get; set; }
        public int LuckyNumber { get; set; }

        public Student()
        {
            // ustawiam puste wartosci na poczatku
            FirstName = "";
            LastName = "";
            IsPresent = true;
            TimesAnswered = 0;
            LuckyNumber = 0;
        }

        public string GetFullName()
        {
            // laczy imie i nazwisko razem
            string fullName = FirstName + " " + LastName;
            return fullName;
        }
    }
}
