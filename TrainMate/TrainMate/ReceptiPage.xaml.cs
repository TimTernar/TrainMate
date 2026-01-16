using Firebase.Database;
using System.Collections.ObjectModel;

namespace TrainMate;

public partial class ReceptiPage : ContentPage
{

    FirebaseClient firebaseClient =
        new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    public ObservableCollection<Recipe> Recepti { get; } = new();

    // your keys
    private readonly string[] keys = { "1", "2", "7" };

    public ReceptiPage()
    {
        InitializeComponent();
        BindingContext = this; 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Recepti.Clear();

        foreach (var k in keys)
        {
            var recipe = await firebaseClient
                .Child($"Recepti/{k}")
                .OnceSingleAsync<Recipe>();

            if (recipe != null)
                Recepti.Add(recipe);
        }
    }

    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Recipe recipe)
            await DisplayAlert(recipe.Name, recipe.Description, "OK");
    }

}