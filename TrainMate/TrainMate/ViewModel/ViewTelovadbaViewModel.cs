using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrainMate.ViewModel;

public class ViewTelovadbaViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public string PageTitle { get; }
    public ObservableCollection<WorkoutExerciseRow> ExerciseRows { get; } = new();

    public ViewTelovadbaViewModel(Workout workout)
    {
        PageTitle = string.IsNullOrWhiteSpace(workout.Name)
            ? $"Workout {workout.CreatedAt}"
            : workout.Name;

        if (workout.Exercises == null || workout.Exercises.Count == 0)
            return;

        foreach (var kvp in workout.Exercises)
        {
            var key = kvp.Key;              // e.g. "bench_press"
            var we = kvp.Value;             // WorkoutExercise

            // Firebase often returns Sets like [null, {...}, {...}] -> remove nulls
            var sets = (we?.Sets ?? new List<ExerciseSet>())
                .Where(s => s != null)
                .ToList();

            ExerciseRows.Add(new WorkoutExerciseRow
            {
                Key = key,
                DisplayName = ToDisplayName(key),
                Sets = new ObservableCollection<ExerciseSet>(sets)
            });
        }
    }

    private static string ToDisplayName(string key)
    {
        // "bench_press" -> "Bench Press"
        var parts = key.Split('_', StringSplitOptions.RemoveEmptyEntries);
        return string.Join(" ", parts.Select(p => char.ToUpper(p[0]) + p.Substring(1)));
    }
}

public class WorkoutExerciseRow
{
    public string Key { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public ObservableCollection<ExerciseSet> Sets { get; set; } = new();
}
