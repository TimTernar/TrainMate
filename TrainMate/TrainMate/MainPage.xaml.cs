using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Firebase.Auth;
using Firebase.Auth.Providers;


namespace TrainMate
{
    public partial class MainPage : ContentPage
    {

        private string webApiKey = "AIzaSyDS2qmCMAA-qgB25cB_Ry7yRd7uurLKfyc";

        public string? email { get; set; }

        public string? password { get; set; }


        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            this.email = emailEntry.Text;
            this.password = passwordEntry.Text;
            try
            {
                var config = new Firebase.Auth.FirebaseAuthConfig
                {
                    ApiKey = webApiKey,
                    AuthDomain = "mobilne-45354.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
                            new EmailProvider()
                    }
                };

                var client = new FirebaseAuthClient(config);
                var userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
                var serializiranaVsebina = System.Text.Json.JsonSerializer.Serialize(userCredential.AuthCredential);

                if (userCredential != null)
                {
                    Preferences.Set("SvezToken", serializiranaVsebina);
                    await Application.Current.MainPage.DisplayAlert("Uspeh", "Prijava uspešna!", "V redu");
                    await Navigation.PushAsync(new HomePage());
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Napaka", ex.Message, "V redu");
            }
        }

        public async void OnGoogleClicked(object sender, EventArgs e)
        {
            try
            {
                var config = new Firebase.Auth.FirebaseAuthConfig
                {
                    ApiKey = webApiKey,
                    AuthDomain = "mobilne-45354.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
                            new EmailProvider()
                    }
                };
                //ni dokončano
                var client = new FirebaseAuthClient(config);
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        public async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());        
        }

    }

}
