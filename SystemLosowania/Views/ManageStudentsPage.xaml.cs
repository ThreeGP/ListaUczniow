using System.Collections.ObjectModel;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class ManageStudentsPage : ContentPage
{
    private readonly string className;
    private readonly FileManager fileManager;
    private SchoolClass schoolClass;
    private readonly ObservableCollection<StudentDisplay> studentDisplayList;

    public ManageStudentsPage(string className)
    {
        InitializeComponent();

        this.className = className;
        fileManager = new FileManager();
        studentDisplayList = new ObservableCollection<StudentDisplay>();
        schoolClass = new SchoolClass(className);

        ClassTitleLabel.Text = "Klasa: " + className;

        LoadStudentsFromFile();
        RefreshStudentsList();
    }

    private void LoadStudentsFromFile()
    {
        SchoolClass? loadedClass = fileManager.LoadClass(className);
        if (loadedClass != null)
        {
            schoolClass = loadedClass;
        }
    }

    private void RefreshStudentsList()
    {
        studentDisplayList.Clear();

        for (int i = 0; i < schoolClass.Students.Count; i++)
        {
            Student student = schoolClass.Students[i];
            StudentDisplay display = new StudentDisplay
            {
                Student = student,
                FullName = student.GetFullName()
            };

            studentDisplayList.Add(display);
        }

        StudentsCollection.ItemsSource = studentDisplayList;
    }

    private async void OnAddStudentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            await DisplayAlert("Blad", "Wpisz imie i nazwisko!", "OK");
            return;
        }

        int nextNumber = schoolClass.Students.Count + 1;

        Student newStudent = new Student
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            LuckyNumber = nextNumber,
            IsPresent = true,
            TimesAnswered = 0
        };

        schoolClass.Students.Add(newStudent);

        FirstNameEntry.Text = string.Empty;
        LastNameEntry.Text = string.Empty;

        RefreshStudentsList();
    }

    private void OnDeleteStudentClicked(object sender, EventArgs e)
    {
        if (sender is not Button deleteButton)
        {
            return;
        }

        if (deleteButton.BindingContext is not StudentDisplay studentDisplay)
        {
            return;
        }

        schoolClass.Students.Remove(studentDisplay.Student);
        RefreshStudentsList();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        fileManager.SaveClass(schoolClass);
        await DisplayAlert("Sukces", "Zmiany zapisane pomyslnie!", "OK");
        await Navigation.PopAsync();
    }

    public class StudentDisplay
    {
        public required Student Student { get; set; }
        public required string FullName { get; set; }
    }
}
