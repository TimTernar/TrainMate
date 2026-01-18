using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrainMate.ViewModel
{
    internal class SetVM : INotifyPropertyChanged
    {
        public ExerciseVM Parent { get; }

        int _setNumber;
        public int SetNumber
        {
            get => _setNumber;
            set { _setNumber = value; OnPropertyChanged(); }
        }

        public string Previous { get; set; }

        string? _kg;
        public string? Kg
        {
            get => _kg;
            set { _kg = value; OnPropertyChanged(); }
        }

        string? _reps;
        public string? Reps
        {
            get => _reps;
            set { _reps = value; OnPropertyChanged(); }
        }

        bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(); }
        }

        public ICommand ToggleDoneCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SetVM(ExerciseVM parent, int setNumber, string previous)
        {
            Parent = parent;
            _setNumber = setNumber;
            Previous = previous;

            ToggleDoneCommand = new Command(() => IsDone = !IsDone);
        }

        void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}