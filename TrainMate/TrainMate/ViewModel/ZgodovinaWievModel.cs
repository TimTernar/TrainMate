using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TrainMate;

public class ZgodovinaViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly FirebaseClient _firebase =
        new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    // Calendar
    public ObservableCollection<DayCell> Days { get; } = new();

    private DateTime _currentMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    public string MonthTitle => _currentMonth.ToString("MMM yyyy", CultureInfo.InvariantCulture);

    // Workouts indexed by date
    private readonly Dictionary<DateTime, List<Workout>> _workoutsByDate = new();

    // Exercise name lookup (bench_press => "Bench Press")
    private readonly Dictionary<string, string> _exerciseNameByKey = new();

    // Selected day
    private DayCell? _selectedDay;
    public DayCell? SelectedDay
    {
        get => _selectedDay;
        set
        {
            if (_selectedDay == value) return;

            // Unselect previous
            if (_selectedDay != null) _selectedDay.IsSelected = false;

            _selectedDay = value;

            if (_selectedDay != null)
                _selectedDay.IsSelected = true;

            OnPropertyChanged();
            UpdateSelectedWorkoutCard();
        }
    }

    // Workout card (below calendar)
    private string _selectedWorkoutTitle = "";
    public string SelectedWorkoutTitle
    {
        get => _selectedWorkoutTitle;
        set { _selectedWorkoutTitle = value; OnPropertyChanged(); }
    }

    public ObservableCollection<WorkoutLine> SelectedWorkoutLines { get; } = new();

    public bool HasSelectedWorkout => SelectedWorkoutLines.Count > 0;

    public Command PrevMonthCommand { get; }
    public Command NextMonthCommand { get; }

    public ZgodovinaViewModel()
    {
        PrevMonthCommand = new Command(() =>
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            BuildMonth();
        });

        NextMonthCommand = new Command(() =>
        {
            _currentMonth = _currentMonth.AddMonths(1);
            BuildMonth();
        });
    }

    public async Task LoadAsync()
    {
        await LoadExerciseCatalogueAsync();
        await LoadWorkoutsAsync();
        BuildMonth();

        // optional: auto-select today
        var todayCell = Days.FirstOrDefault(d => d.Date.Date == DateTime.Today.Date);
        if (todayCell != null) SelectedDay = todayCell;
    }

    private async Task LoadExerciseCatalogueAsync()
    {
        _exerciseNameByKey.Clear();

        // /Exercises is an object (keys: bench_press, ...)
        // Use a small DTO so we don't fight your Exercise model shape.
        var nodes = await _firebase.Child("Exercises").OnceAsync<ExerciseDb>();

        foreach (var n in nodes)
        {
            if (n.Object?.Name == null) continue;
            _exerciseNameByKey[n.Key] = n.Object.Name;
        }
    }

    private async Task LoadWorkoutsAsync()
    {
        _workoutsByDate.Clear();

        // /Workouts is an ARRAY in your Firebase
        var list = await _firebase.Child("Workouts").OnceSingleAsync<List<Workout>>();

        foreach (var w in (list ?? new List<Workout>()))
        {
            if (w == null) continue;

            // CreatedAt is "yyyy-MM-dd" in your JSON
            if (!DateTime.TryParse(w.CreatedAt, out var dt))
                continue;

            var day = dt.Date;

            if (!_workoutsByDate.TryGetValue(day, out var bucket))
            {
                bucket = new List<Workout>();
                _workoutsByDate[day] = bucket;
            }

            bucket.Add(w);
        }
    }

    private void BuildMonth()
    {
        Days.Clear();

        var firstOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);

        // Sunday-first to match Su–Sa headers
        int sundayIndex = (int)firstOfMonth.DayOfWeek;
        var start = firstOfMonth.AddDays(-sundayIndex);

        for (int i = 0; i < 42; i++)
        {
            var date = start.AddDays(i).Date;
            bool isCurrentMonth = date.Month == _currentMonth.Month;

            Days.Add(new DayCell
            {
                Date = date,
                DayNumber = date.Day.ToString(),
                IsCurrentMonth = isCurrentMonth,
                HasWorkout = _workoutsByDate.ContainsKey(date)
            });
        }

        OnPropertyChanged(nameof(MonthTitle));

        // Clear selection/workout card when changing month
        SelectedWorkoutLines.Clear();
        OnPropertyChanged(nameof(HasSelectedWorkout));
        SelectedDay = null;
    }

    private void UpdateSelectedWorkoutCard()
    {
        SelectedWorkoutLines.Clear();

        if (SelectedDay == null)
        {
            OnPropertyChanged(nameof(HasSelectedWorkout));
            return;
        }

        var date = SelectedDay.Date.Date;

        if (!_workoutsByDate.TryGetValue(date, out var workouts) || workouts.Count == 0)
        {
            OnPropertyChanged(nameof(HasSelectedWorkout));
            return;
        }

        // "load one of the 2 saved workouts" -> pick the first workout of that day
        var workout = workouts[0];

        // Title like: "Sep. 9, 2025 “Leg day”"
        var dateTitle = date.ToString("MMM. d, yyyy", CultureInfo.InvariantCulture);
        var wname = string.IsNullOrWhiteSpace(workout.Name) ? "Workout" : workout.Name;
        SelectedWorkoutTitle = $"{dateTitle} “{wname}”";

        // Convert workout.Exercises to display lines
        if (workout.Exercises != null)
        {
            foreach (var kvp in workout.Exercises)
            {
                var key = kvp.Key; // bench_press
                var we = kvp.Value;

                var sets = (we?.Sets ?? new List<ExerciseSet>())
                    .Where(s => s != null)
                    .ToList();

                int setCount = sets.Count;

                // Use MAX weight as the displayed weight (like 30kg)
                int weight = setCount > 0 ? sets.Max(s => s.Weight) : 0;

                var exName = _exerciseNameByKey.TryGetValue(key, out var nameFromDb)
                    ? nameFromDb
                    : ToDisplayName(key);

                SelectedWorkoutLines.Add(new WorkoutLine
                {
                    ExerciseName = exName,
                    SetsText = setCount > 0 ? $"{setCount}x" : "",
                    WeightText = weight > 0 ? $"{weight}kg" : ""
                });
            }
        }

        OnPropertyChanged(nameof(HasSelectedWorkout));
    }

    private static string ToDisplayName(string key)
        => string.Join(" ", key.Split('_', StringSplitOptions.RemoveEmptyEntries)
            .Select(w => char.ToUpper(w[0]) + w.Substring(1)));

    // DTO: only what we need from /Exercises
    private class ExerciseDb
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

public class WorkoutLine
{
    public string ExerciseName { get; set; } = "";
    public string SetsText { get; set; } = "";
    public string WeightText { get; set; } = "";
}
