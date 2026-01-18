using Firebase.Database;
using TrainMate.ViewModel;
using Firebase.Database.Query;

namespace TrainMate;

public partial class DodajTelovadboPage : ContentPage
{
	public DodajTelovadboPage()
	{
		InitializeComponent();
	}

    private readonly FirebaseClient firebaseClient =
    new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    private List<ExerciseItem> _items = new();

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // This works because /Exercises is an object with children keys
        var nodes = await firebaseClient
            .Child("Exercises")
            .OnceAsync<Exercise>();

        _items = nodes
            .Where(n => n.Object != null)
            .Select(n => new ExerciseItem
            {
                Key = n.Key,
                Exercise = n.Object
            })
            .OrderBy(x => x.Name)
            .ToList();

        ExerciseList.ItemsSource = _items;
    }

    private async void ExerciseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as ExerciseItem;
        if (selected == null)
            return;

        ExerciseList.SelectedItem = null;

        await DisplayAlert("Selected", $"{selected.Name} ({selected.Key})", "OK");

        await Navigation.PopAsync();
    }

}