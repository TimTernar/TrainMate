using Firebase.Database;
using Firebase.Database.Query;
using TrainMate.ViewModel;

namespace TrainMate;

public partial class DodajTelovadboPage : ContentPage
{
    private readonly FirebaseClient firebaseClient =
        new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    private List<ExerciseItem> _items = new();
    private readonly TaskCompletionSource<ExerciseItem?> _tcs;

    public DodajTelovadboPage(TaskCompletionSource<ExerciseItem?> tcs)
    {
        InitializeComponent();
        _tcs = tcs;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

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

        _tcs.TrySetResult(selected);

        await Navigation.PopAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _tcs.TrySetResult(null);
    }
}
