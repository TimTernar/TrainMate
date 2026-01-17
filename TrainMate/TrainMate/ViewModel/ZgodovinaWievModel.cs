using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace TrainMate;

public class ZgodovinaViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ObservableCollection<DayCell> Days { get; } = new();

    private DateTime _currentMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);

    public string MonthTitle => _currentMonth.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

    private DayCell? _selectedDay;
    public DayCell? SelectedDay
    {
        get => _selectedDay;
        set
        {
            _selectedDay = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SelectedDayText));
        }
    }

    public string SelectedDayText =>
        SelectedDay?.Date is DateTime d
            ? $"Selected: {d:dddd, dd MMMM yyyy}"
            : "Select a day";

    public ICommand PrevMonthCommand { get; }
    public ICommand NextMonthCommand { get; }

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

        BuildMonth();
    }

    private void BuildMonth()
    {
        Days.Clear();

        var firstOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);

        // Sunday(Su=0 ... Sa=6)
        int sundayIndex = (int)firstOfMonth.DayOfWeek;

        var start = firstOfMonth.AddDays(-sundayIndex);

        for (int i = 0; i < 42; i++)
        {
            var date = start.AddDays(i);
            bool isCurrentMonth = date.Month == _currentMonth.Month;

            Days.Add(new DayCell
            {
                Date = date,
                DayNumber = date.Day.ToString(),
                IsCurrentMonth = isCurrentMonth,
                Opacity = isCurrentMonth ? 1.0 : 0.35
            });
        }

        SelectedDay = null;
        OnPropertyChanged(nameof(MonthTitle));
    }
}
