using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using TrainMate.ViewModel;

namespace TrainMate;

public partial class ViewTelovadba : ContentPage
{
    public ViewTelovadba(Workout workout)
    {
        InitializeComponent();
        BindingContext = new ViewTelovadbaViewModel(workout);
    }
}
