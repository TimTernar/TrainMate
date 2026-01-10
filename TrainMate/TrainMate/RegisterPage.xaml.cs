using Firebase.Auth.Providers;
using Firebase.Auth;


namespace TrainMate;

public partial class RegisterPage : ContentPage
{

    public string webApiKey = "AIzaSyDS2qmCMAA-qgB25cB_Ry7yRd7uurLKfyc";

    private INavigation navigation;

    public Command RegistracijaUporabnika { get; }

    public string email { get; set; }

    public string geslo { get; set; }

    private Entry _eTxEmail;
    private Entry _eTxPass;

    public RegisterPage()
	{
		InitializeComponent();
    }

	public async void OnContinueClicked(object sender, EventArgs e)
	{

        this.email = emailEntry.Text;
        this.geslo = passwordEntry.Text;

        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(geslo))
            {
                await DisplayAlert("Error", "All fields are required.", "Ok");
                return;
            }

            var config = new FirebaseAuthConfig
            {
                ApiKey = webApiKey,
                AuthDomain = "mobilne-45354.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                            new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);
            var userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, geslo);

            if (userCredential != null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "User sucefully registered!", "Ok");
                await Navigation.PushAsync(new AnketaPage1());
            }
        }
        catch (FirebaseAuthException ex)
        {
            var message = ex.Reason switch
            {
                AuthErrorReason.EmailExists => "E-mail is already registered.",
                AuthErrorReason.WeakPassword => "Password is too weak.",
                _ => ex.Message
            };

            await DisplayAlert("Error", message, "Ok");
        }
    }
}
