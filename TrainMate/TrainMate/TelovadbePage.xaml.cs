namespace TrainMate;

public partial class TelovadbePage : ContentPage
{
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

        var names = new List<string>();

        if (response.TrimStart().StartsWith("["))
        {
            var workouts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(response);
            foreach (var w in workouts)
                names.Add((string)w.Name);
        }
        else
        {
            var workouts = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);
            foreach (var w in workouts.Values)
                names.Add((string)w.Name);
        }

        WorkoutList.ItemsSource = names;
    }

    private async void AddWorkout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTelovadba());
    }
}
