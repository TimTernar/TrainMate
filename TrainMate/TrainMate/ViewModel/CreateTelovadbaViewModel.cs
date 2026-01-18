using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrainMate.ViewModel
{
    internal class CreateTelovadbaViewModel
    {
        public ObservableCollection<ExerciseVM> Exercises { get; } = new();

        public ICommand AddExerciseCommand { get; }
        public ICommand DeleteWorkoutCommand { get; }
        public ICommand DeleteSetCommand { get; }

        public CreateTelovadbaViewModel()
        {
            // Demo podatki (lahko odstraniš)
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

            AddExerciseCommand = new Command(() =>
            {
                Exercises.Add(new ExerciseVM(this) { Name = "New Exercise" });
            });

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
        }

    }
}
