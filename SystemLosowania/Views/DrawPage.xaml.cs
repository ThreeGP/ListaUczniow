using Microsoft.Maui.Controls.Xaml;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class DrawPage : ContentPage
{
    private readonly string className;
    private readonly FileManager fileManager;
    private readonly DrawManager drawManager;
    private SchoolClass schoolClass;
    private bool isAnimating;

    public DrawPage(string className)
    {
        this.LoadFromXaml(typeof(DrawPage));

        this.className = className;
        fileManager = new FileManager();
        drawManager = new DrawManager();
        schoolClass = new SchoolClass(className);

        LoadClass();
    }

    private void LoadClass()
    {
        SchoolClass loadedClass = fileManager.LoadClass(className);
        if (loadedClass != null)
        {
            schoolClass = loadedClass;
        }

        Label classNameLabel = this.FindByName<Label>("ClassNameLabel");
        if (classNameLabel != null)
        {
            classNameLabel.Text = "Klasa: " + className;
        }
    }

    private async void OnAttendanceClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AttendancePage(className));
    }

    private async void OnDrawClicked(object sender, EventArgs e)
    {
        if (isAnimating)
        {
            return;
        }

        if (schoolClass.Students.Count == 0)
        {
            await DisplayAlert("Blad", "Brak uczniow w tej klasie!", "OK");
            return;
        }

        Entry luckyNumberEntry = this.FindByName<Entry>("LuckyNumberEntry");
        Button drawButtonMain = this.FindByName<Button>("DrawButtonMain");
        Label rollingNameLabel = this.FindByName<Label>("RollingNameLabel");
        VerticalStackLayout resultSection = this.FindByName<VerticalStackLayout>("ResultSection");

        int luckyNumber = 0;
        if (luckyNumberEntry != null)
        {
            int.TryParse(luckyNumberEntry.Text, out luckyNumber);
        }

        Student drawnStudent = drawManager.DrawStudent(schoolClass, luckyNumber);
        if (drawnStudent == null)
        {
            await DisplayAlert("Blad", "Brak dostepnych uczniow (wszyscy nieobecni)!", "OK");
            return;
        }

        isAnimating = true;

        if (drawButtonMain != null)
        {
            drawButtonMain.IsEnabled = false;
        }

        if (resultSection != null)
        {
            resultSection.IsVisible = false;
        }

        if (rollingNameLabel != null)
        {
            List<Student> pool = schoolClass.Students;
            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                Student current = pool[random.Next(pool.Count)];
                rollingNameLabel.Text = current.GetFullName();
                await Task.Delay(80 + i * 15);
            }

            rollingNameLabel.Text = drawnStudent.GetFullName();
        }

        await ShowResult(drawnStudent, luckyNumber);

        isAnimating = false;

        if (drawButtonMain != null)
        {
            drawButtonMain.IsEnabled = true;
        }
    }

    private async Task ShowResult(Student student, int luckyNumber)
    {
        Label resultNameLabel = this.FindByName<Label>("ResultNameLabel");
        Label resultInfoLabel = this.FindByName<Label>("ResultInfoLabel");
        VerticalStackLayout resultSection = this.FindByName<VerticalStackLayout>("ResultSection");

        if (resultNameLabel != null)
        {
            resultNameLabel.Text = student.GetFullName();
        }

        string info = "Numer: " + student.LuckyNumber + " | Odpowiadal: " + student.TimesAnswered + "/3";
        if (student.LuckyNumber == luckyNumber && luckyNumber > 0)
        {
            info += " | SZCZESLIWY NUMER";
        }

        if (resultInfoLabel != null)
        {
            resultInfoLabel.Text = info;
        }

        if (resultSection != null)
        {
            resultSection.IsVisible = true;
        }

        await Task.Delay(100);
    }

    private async void OnSaveAndReturnClicked(object sender, EventArgs e)
    {
        fileManager.SaveClass(schoolClass);
        await Navigation.PopToRootAsync();
    }
}
