using Microsoft.Maui.Controls.Xaml;
using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class SelectClassPage : ContentPage
{
    private readonly FileManager fileManager;

    public SelectClassPage()
    {
        this.LoadFromXaml(typeof(SelectClassPage));
        fileManager = new FileManager();
        LoadClasses();
    }

    private void LoadClasses()
    {
        List<string> classNames = fileManager.GetAllClassNames();

        CollectionView classListCollection = this.FindByName<CollectionView>("ClassListCollection");
        if (classListCollection != null)
        {
            classListCollection.ItemsSource = classNames;
        }

        Label noClassesLabel = this.FindByName<Label>("NoClassesLabel");
        if (noClassesLabel != null)
        {
            noClassesLabel.IsVisible = classNames.Count == 0;
        }
    }

    private async void OnClassSelected(object sender, EventArgs e)
    {
        if (sender is not Frame frame)
        {
            return;
        }

        if (frame.BindingContext is not string className)
        {
            return;
        }

        await Navigation.PushAsync(new DrawPage(className));
    }
}
