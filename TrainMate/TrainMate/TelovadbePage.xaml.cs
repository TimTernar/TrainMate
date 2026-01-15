using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TrainMate;

public partial class TelovadbePage : ContentPage
{
    public ObservableCollection<Workout> Workouts { get; set; } = new();

    public TelovadbePage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadWorkoutsAsync();
    }

    //tole te povede na stran CreateTelovadba ali "Create Workout"
    private async void AddWorkout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTelovadba());
    }

    //baloži podatke o obstojeèih treningih iz firebase database-a
    private async Task LoadWorkoutsAsync()
    {
        try
        {
            using HttpClient httpClient = new();
            string url =
                "https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/Workouts.json";
            var response = await httpClient.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<List<Workout>>(response);

            if (data != null && data.Count > 0)
            {
                Workouts.Clear();
                foreach (var workout in data)
                {
                    if (workout != null)
                    {
                        if (workout.Exercises != null)
                        {
                            foreach (var exercise in workout.Exercises.Values)
                            {
                                if (exercise.Sets != null)
                                {
                                    exercise.Sets = exercise.Sets.Where(s => s != null).ToList();
                                }
                            }
                        }
                        Workouts.Add(workout);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Napaka", $"{ex.Message}", "V redu");
        }
    }
}