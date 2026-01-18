namespace TrainMate;

public partial class ZgodovinaPage : ContentPage
{
    private readonly ZgodovinaViewModel _vm = new();

    public ZgodovinaPage()
    {
        InitializeComponent();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}