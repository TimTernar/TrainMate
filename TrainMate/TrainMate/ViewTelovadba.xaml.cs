using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace TrainMate;

public partial class ViewTelovadba : ContentPage
{
    public ObservableCollection<dynamic> Workouts { get; } = new();

    public ViewTelovadba(dynamic workout)
    {
        InitializeComponent();
        BindingContext = this;

        var fixedWorkout = new
        {
            Name = workout.Name,
            Exercises = ((JObject)workout.Exercises)
                .Properties()
                .ToDictionary(p => p.Name, p => p.Value)
        };

        Workouts.Add(fixedWorkout);
    }
}
