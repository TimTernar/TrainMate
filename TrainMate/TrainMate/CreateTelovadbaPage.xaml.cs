using TrainMate.ViewModel;

namespace TrainMate;

public partial class CreateTelovadbaPage : ContentPage
{
    public CreateTelovadbaPage()
    {
        InitializeComponent();
        BindingContext = new CreateTelovadbaViewModel();
    }
}
