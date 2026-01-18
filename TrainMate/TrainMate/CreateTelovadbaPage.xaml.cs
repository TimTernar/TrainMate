using Firebase.Database;
using Firebase.Database.Query;
using TrainMate.ViewModel;

namespace TrainMate;

public partial class CreateTelovadbaPage : ContentPage
{
    private readonly FirebaseClient firebaseClient =
        new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    public CreateTelovadbaPage()
    {
        InitializeComponent();
        BindingContext = new CreateTelovadbaViewModel(); 
    }

    private static int TryParseInt(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        return int.TryParse(value, out var n) ? n : 0;
    }


    private async void Done_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is not CreateTelovadbaViewModel vm)
            return;

        // Basic validation
        if (string.IsNullOrWhiteSpace(vm.WorkoutName))
        {
            await DisplayAlert("Missing name", "Please enter a workout name.", "OK");
            return;
        }

        if (vm.Exercises.Count == 0)
        {
            await DisplayAlert("No exercises", "Add at least one exercise.", "OK");
            return;
        }

        var workout = new Workout
        {
            Name = vm.WorkoutName.Trim(),
            Description = vm.WorkoutDescription?.Trim(),
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
            Exercises = new Dictionary<string, WorkoutExercise>()
        };

        foreach (var ex in vm.Exercises)
        {
            // POMEMBNO: Shrani v firebase key (behnc_press ...) 
            if (string.IsNullOrWhiteSpace(ex.Key))
                continue;

            var sets = ex.Sets
                .Select(s => new ExerciseSet
                {
                    Weight = TryParseInt(s.Kg),
                    Reps = TryParseInt(s.Reps)
                })
                .ToList();

            workout.Exercises[ex.Key] = new WorkoutExercise
            {
                Sets = sets
            };
        }

        // Save to Firebase
        try
        {
            var result = await firebaseClient
                .Child("Workouts")
                .PostAsync(workout);

            workout.Id = result.Key; // shrani v firebase key

            await DisplayAlert("Saved", "Workout saved to Firebase.", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }


    }

    private async void AddExercise_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is not CreateTelovadbaViewModel vm)
            return;

        var tcs = new TaskCompletionSource<ExerciseItem?>();
        await Navigation.PushAsync(new DodajTelovadboPage(tcs));

        var selected = await tcs.Task;
        if (selected == null)
            return;

        vm.AddExerciseFromSelection(selected);
    }
}
