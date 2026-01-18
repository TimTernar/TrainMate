using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    public class DayCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DateTime Date { get; set; }
        public string DayNumber { get; set; } = "";
        public bool IsCurrentMonth { get; set; }

        private bool _hasWorkout;
        public bool HasWorkout
        {
            get => _hasWorkout;
            set { _hasWorkout = value; OnPropertyChanged(); OnPropertyChanged(nameof(BackgroundColor)); OnPropertyChanged(nameof(TextColor)); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); OnPropertyChanged(nameof(BackgroundColor)); OnPropertyChanged(nameof(TextColor)); }
        }

        public Color BackgroundColor
        {
            get
            {
                if (IsSelected) return Color.FromArgb("#2B2B2B");         
                if (HasWorkout) return Color.FromArgb("#F1F1F1");         
                return Colors.Transparent;
            }
        }

        public Color TextColor
        {
            get
            {
                if (!IsCurrentMonth) return Color.FromArgb("#999999");
                if (IsSelected) return Colors.White;
                return Colors.Black;
            }
        }

        public double Opacity => IsCurrentMonth ? 1.0 : 0.35;
    }
}
