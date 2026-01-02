namespace TrainMate;

public partial class AnketaPage1 : ContentPage
{
	public AnketaPage1()
	{
		InitializeComponent();
	}

    public async void OnContinueClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }

}