using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class AttendancePage : ContentPage
{
    private readonly string className;
    private SchoolClass schoolClass;
    private readonly FileManager fileManager;
    private readonly ObservableCollection<StudentAttendance> attendanceList;

    public AttendancePage(string className)
    {
        this.LoadFromXaml(typeof(AttendancePage));

        this.className = className;
        fileManager = new FileManager();
        attendanceList = new ObservableCollection<StudentAttendance>();
        schoolClass = new SchoolClass(className);

        Label classTitleLabel = this.FindByName<Label>("ClassTitleLabel");
        if (classTitleLabel != null)
        {
            classTitleLabel.Text = "Klasa: " + className;
        }

        LoadAttendance();
    }

    private void LoadAttendance()
    {
        SchoolClass loadedClass = fileManager.LoadClass(className);
        if (loadedClass == null || loadedClass.Students.Count == 0)
        {
            return;
        }

        schoolClass = loadedClass;
        attendanceList.Clear();

        for (int i = 0; i < schoolClass.Students.Count; i++)
        {
            Student student = schoolClass.Students[i];
            StudentAttendance attendance = new StudentAttendance
            {
                Student = student,
                FullName = student.GetFullName(),
                IsPresent = student.IsPresent
            };

            attendanceList.Add(attendance);
        }

        CollectionView attendanceCollection = this.FindByName<CollectionView>("AttendanceCollection");
        if (attendanceCollection != null)
        {
            attendanceCollection.ItemsSource = attendanceList;
        }
    }

    private async void OnSaveAttendanceClicked(object sender, EventArgs e)
    {
        for (int i = 0; i < attendanceList.Count; i++)
        {
            StudentAttendance attendance = attendanceList[i];
            attendance.Student.IsPresent = attendance.IsPresent;
        }

        fileManager.SaveClass(schoolClass);

        await DisplayAlert("Sukces", "Obecnosc zapisana!", "OK");
        await Navigation.PopAsync();
    }

    public class StudentAttendance
    {
        public required Student Student { get; set; }
        public required string FullName { get; set; }
        public bool IsPresent { get; set; }
    }
}
