namespace TrainMate;

public partial class ReceptShowPage : ContentPage
{
    public ReceptShowPage(Recipe recipe)
    {
        InitializeComponent();
        BindingContext = recipe;
    }
}