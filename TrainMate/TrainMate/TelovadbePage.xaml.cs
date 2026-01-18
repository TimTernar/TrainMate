using Newtonsoft.Json;

namespace TrainMate;

public partial class TelovadbePage : ContentPage
{
    private List<Workout> _workouts = new();

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

        if (string.IsNullOrWhiteSpace(response) || response == "null")
        {
            WorkoutList.ItemsSource = null;
            return;
        }

        // If Firebase returns an array: [ {..}, {..} ]
        if (response.TrimStart().StartsWith("["))
        {
            var list = JsonConvert.DeserializeObject<List<Workout>>(response);
            if (list != null)
                _workouts.AddRange(list);
        }
        // If Firebase returns an object: { "id1": {..}, "id2": {..} }
        else
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, Workout>>(response);
            if (dict != null)
            {
                foreach (var kvp in dict)
                {
                    kvp.Value.Id = kvp.Key; // store firebase key as Id
                    _workouts.Add(kvp.Value);
                }
            }
        }

        // OPTIONAL: if you REALLY want ONLY 0 and 1:
        // WorkoutList.ItemsSource = _workouts.Take(2).ToList();

        // Normal: show all
        WorkoutList.ItemsSource = _workouts;
    }

    private async void WorkoutList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var workout = (Workout)e.CurrentSelection[0];
            await Navigation.PushAsync(new ViewTelovadba(workout));
        }

        ((CollectionView)sender).SelectedItem = null;
    }

    private async void AddWorkout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTelovadbaPage());
    }
}
