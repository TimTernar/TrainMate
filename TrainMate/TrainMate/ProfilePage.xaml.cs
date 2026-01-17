using Firebase.Database;
using Firebase.Database.Query;

namespace TrainMate;

public partial class ProfilePage : ContentPage
{

    private readonly ProfilePageViewModel _vm = new();

    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }

    private async void OnSavedWorkoutsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TelovadbePage());
    }

    private async void OnRecepiesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReceptiPage());
    }

    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {
        if (sender is BindableObject bo && bo.BindingContext is Recipe recipe)
        {
            await DisplayAlert("Recipe", recipe.Name, "OK");
        }
    }
}