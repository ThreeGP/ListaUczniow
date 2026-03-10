using Microsoft.Maui.Controls.Xaml;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class MainMenuPage : ContentPage
{
    private readonly FileManager fileManager;

    public MainMenuPage()
    {
        this.LoadFromXaml(typeof(MainMenuPage));
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
        CollectionView? classesCollection = this.FindByName<CollectionView>("ClassesCollection");

        if (classesCollection != null)
        {
            classesCollection.ItemsSource = classNames;
        }
    }

    private async void OnManageClassesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageClassesPage());
    }

    private async void OnStartDrawingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectClassPage());
    }

    private async void OnManageStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectClassForEditPage());
    }
}
