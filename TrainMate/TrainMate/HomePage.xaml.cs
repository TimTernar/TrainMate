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

    private async void OnRecepiesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReceptiPage());
    }

    private async void OnHistoryTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ZgodovinaPage());
    }

    private async void OnStatsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatsPage());
    }

}