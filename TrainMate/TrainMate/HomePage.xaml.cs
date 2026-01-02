namespace TrainMate;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnSavedWorkoutsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TelovadbePage());
    }

}