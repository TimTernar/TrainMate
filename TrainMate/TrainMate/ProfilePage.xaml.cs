using Firebase.Database;
using Firebase.Database.Query;

namespace TrainMate;

public partial class ProfilePage : ContentPage
{

    FirebaseClient firebaseClient = new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");

    public ProfilePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // directly fetch Users/1
        var user = await firebaseClient
            .Child("Users")
            .Child("1")
            .OnceSingleAsync<UserProfile>();

        BindingContext = user;
    }


    private async void OnSavedWorkoutsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TelovadbePage());
    }

    private async void OnRecepiesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReceptiPage());
    }


}