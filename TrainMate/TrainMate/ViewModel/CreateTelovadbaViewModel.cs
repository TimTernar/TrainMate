using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TrainMate.ViewModel
{
    internal class CreateTelovadbaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<ExerciseVM> Exercises { get; } = new();

        // NEW: workout header fields
        private string _workoutName = "";
        public string WorkoutName
        {
            get => _workoutName;
            set
            {
                if (_workoutName == value) return;
                _workoutName = value;
                OnPropertyChanged();
            }
        }

        private string _workoutDescription = "";
        public string WorkoutDescription
        {
            get => _workoutDescription;
            set
            {
                if (_workoutDescription == value) return;
                _workoutDescription = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteWorkoutCommand { get; }
        public ICommand DeleteSetCommand { get; }

        public ICommand SaveWorkoutCommand { get; }

        //demo podatki
        public CreateTelovadbaViewModel()
        {
            var squat = new ExerciseVM(this) { Name = "Squat (Barbell)" };
            squat.AddSet("30 kg x 10");
            squat.AddSet("30 kg x 10");
            squat.AddSet("30 kg x 10");
            squat.AddSet("30 kg x 10");

            var deadlift = new ExerciseVM(this) { Name = "Deadlift" };
            deadlift.AddSet("25 kg x 10");
            deadlift.AddSet("25 kg x 10");
            deadlift.AddSet("25 kg x 10");
            deadlift.AddSet("25 kg x 8");

            Exercises.Add(squat);
            Exercises.Add(deadlift);

            WorkoutName = "My Workout";
            WorkoutDescription = "Notes...";


            DeleteWorkoutCommand = new Command<ExerciseVM>(ex =>
            {
                if (ex == null) return;
                Exercises.Remove(ex);
            });

            DeleteSetCommand = new Command<SetVM>(set =>
            {
                if (set?.Parent == null) return;
                set.Parent.Sets.Remove(set);
                set.Parent.RenumberSets();
            });

            SaveWorkoutCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(WorkoutName))
                {
                    await App.Current.MainPage.DisplayAlert("Missing name", "Please enter a workout name.", "OK");
                    return;
                }

                if (Exercises.Count == 0)
                {
                    await App.Current.MainPage.DisplayAlert("No exercises", "Add at least one exercise.", "OK");
                    return;
                }

                // Later: convert Exercises -> Workout model and post to Firebase
                await App.Current.MainPage.DisplayAlert("Saved (demo)",
                    $"Workout: {WorkoutName}\nExercises: {Exercises.Count}",
                    "OK");
            });
        }
    }
}
