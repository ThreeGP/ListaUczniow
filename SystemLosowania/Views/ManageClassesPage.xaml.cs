using Microsoft.Maui.Controls.Xaml;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class ManageClassesPage : ContentPage
{
    private readonly FileManager fileManager;

    public ManageClassesPage()
    {
        this.LoadFromXaml(typeof(ManageClassesPage));
        fileManager = new FileManager();
        LoadClasses();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadClasses();
    }

    private void LoadClasses()
    {
        List<string> classNames = fileManager.GetAllClassNames();
        CollectionView? existingClassesCollection = this.FindByName<CollectionView>("ExistingClassesCollection");

        if (existingClassesCollection != null)
        {
            existingClassesCollection.ItemsSource = classNames;
        }
    }

    private async void OnCreateClassClicked(object sender, EventArgs e)
    {
        Entry? classNameEntry = this.FindByName<Entry>("ClassNameEntry");
        if (classNameEntry == null)
        {
            return;
        }

        string className = classNameEntry.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(className))
        {
            await DisplayAlert("Blad", "Wpisz nazwe klasy!", "OK");
            return;
        }

        SchoolClass newClass = new SchoolClass(className);
        fileManager.SaveClass(newClass);

        classNameEntry.Text = string.Empty;
        LoadClasses();

        await DisplayAlert("Sukces", "Klasa utworzona pomyslnie!", "OK");
    }

    private async void OnDeleteClassClicked(object sender, EventArgs e)
    {
        if (sender is not Button deleteButton)
        {
            return;
        }

        if (deleteButton.BindingContext is not string className)
        {
            return;
        }

        bool confirm = await DisplayAlert(
            "Potwierdzenie",
            "Czy na pewno chcesz usunac klase: " + className + "?",
            "Tak",
            "Nie");

        if (!confirm)
        {
            return;
        }

        fileManager.DeleteClass(className);
        LoadClasses();
    }
}
