using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;


namespace TrainMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        public async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());        
        }

    }

}
