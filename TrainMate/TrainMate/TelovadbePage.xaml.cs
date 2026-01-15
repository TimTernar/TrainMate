using Newtonsoft.Json;

namespace TrainMate;

public partial class TelovadbePage : ContentPage
{
    private List<dynamic> _workouts = new();

    public TelovadbePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadWorkoutsAsync();
    }

    private async Task LoadWorkoutsAsync()
    {
        using HttpClient httpClient = new();
        string url =
            "https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/Workouts.json";

        var response = await httpClient.GetStringAsync(url);

        _workouts.Clear();

        if (response.TrimStart().StartsWith("["))
        {
            var list = JsonConvert.DeserializeObject<List<dynamic>>(response);
            _workouts.AddRange(list);
        }
        else
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);
            _workouts.AddRange(dict.Values);
        }

        // IMPORTANT: bind FULL workouts, not names
        WorkoutList.ItemsSource = _workouts;
    }

    private async void WorkoutList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var workout = e.CurrentSelection[0]; // FULL WORKOUT OBJECT
            await Navigation.PushAsync(new ViewTelovadba(workout));
        }

        ((CollectionView)sender).SelectedItem = null;
    }

    private async void AddWorkout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTelovadba());
    }
}
