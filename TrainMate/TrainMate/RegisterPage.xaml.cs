namespace TrainMate;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	public async void OnContinueClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AnketaPage1());
	}
}