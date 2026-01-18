using TrainMate.ViewModel;

namespace TrainMate;

public partial class CreateTelovadbaPage : ContentPage
{
    public CreateTelovadbaPage()
    {
        InitializeComponent();
        BindingContext = new CreateTelovadbaViewModel();
    }

    private async void OnSavedWorkoutsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DodajTelovadboPage());
    }
}
